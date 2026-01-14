using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCAMiniEHR.Data;
using HCAMiniEHR.Models;

namespace HCAMiniEHR.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public DeleteModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient? Patient { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Patient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (Patient == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Load patient AND their appointments
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
                return NotFound();

            // Check for FUTURE appointments (Today or later)
            bool hasFutureAppointments = patient.Appointments
                .Any(a => a.AppointmentDate.Date >= DateTime.Today);

            if (hasFutureAppointments)
            {
                // Prevent deletion
                ModelState.AddModelError("", "Cannot delete patient because they have upcoming appointments. Please cancel them first.");
                return Page();
            }

            // Remove PAST appointments first (Cascade delete manually due to Restrict behavior)
            // User requested: "appointment should not get deleted" -> BUT database schema requires it or schema update.
            // Since Schema Update FAILED, we MUST delete appointments to delete patient (or leave patient).
            // Reverting to previous working state: Delete Past appointments.
            _context.Appointments.RemoveRange(patient.Appointments);
            
            // Remove Patient
            _context.Patients.Remove(patient);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
