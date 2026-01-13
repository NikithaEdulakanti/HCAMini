using HCAMiniEHR.Data;
using HCAMiniEHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public CreateModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        // =========================
        // BINDINGS
        // =========================
        [BindProperty]
        public Appointment Appointment { get; set; } = new();

        public Patient? Patient { get; set; }

        public List<SelectListItem> Doctors { get; set; } = new();

        // =========================
        // GET
        // =========================
        public async Task<IActionResult> OnGetAsync(int patientId)
        {
            // Load patient
            Patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == patientId);

            if (Patient == null)
                return NotFound();

            // Prepare appointment
            Appointment.PatientId = patientId;
            Appointment.AppointmentDate = DateTime.Today;

            await LoadDoctorsAsync();
            return Page();
        }

        // =========================
        // POST
        // =========================
        public async Task<IActionResult> OnPostAsync()
        {
            // ?? VERY IMPORTANT
            Patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == Appointment.PatientId);

            if (Patient == null)
                return NotFound();

            await LoadDoctorsAsync();

            if (!ModelState.IsValid)
                return Page();

            Appointment.Status = "Scheduled";
            Appointment.CreatedAt = DateTime.Now;

            _context.Appointments.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Appointments/Index",
                new { patientId = Appointment.PatientId });
        }

        // =========================
        // Helper
        // =========================
        private async Task LoadDoctorsAsync()
        {
            Doctors = await _context.Doctors
                .Where(d => d.IsActive)
                .Select(d => new SelectListItem
                {
                    Value = d.DoctorId.ToString(),
                    Text = d.FullName
                })
                .ToListAsync();
        }
    }
}
