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
        public List<SelectListItem> Patients { get; set; } = new();

        // =========================
        // GET
        // =========================
        public async Task<IActionResult> OnGetAsync(int? patientId)
        {
            await LoadPatientsAsync();
            await LoadDoctorsAsync();

            if (patientId.HasValue)
            {
                Appointment.PatientId = patientId.Value;
            }

            // Default to tomorrow for booking
            Appointment.AppointmentDate = DateTime.Today.AddDays(1);
            
            return Page();
        }

        // =========================
        // POST
        // =========================
        public async Task<IActionResult> OnPostAsync()
        {
            await LoadDoctorsAsync();
            await LoadPatientsAsync();

            // Ignore validation for navigation properties that are not bound
            ModelState.Remove("Appointment.Patient");
            ModelState.Remove("Appointment.Doctor");
            ModelState.Remove("Appointment.LabOrders");

            if (!ModelState.IsValid)
                return Page();

            // Combine Date and Time
            var fullDateTime = Appointment.AppointmentDate.Date + Appointment.AppointmentTime;
            Appointment.AppointmentDate = fullDateTime;

            // 1. Validate Future Date (More than today)
            // User requirement: "appointment should schedule for more that todays date not less then that"
            if (Appointment.AppointmentDate.Date <= DateTime.Today)
            {
                ModelState.AddModelError("Appointment.AppointmentDate", "Appointment must be scheduled for a future date (after today).");
                return Page();
            }

            // 2. Conflict Detection (Same doctor, within 1 hour)
            // User requirement: "same doctor on the same day within before the after 1 hour"
            var conflictStartTime = fullDateTime.AddHours(-1);
            var conflictEndTime = fullDateTime.AddHours(1);

            bool hasConflict = await _context.Appointments
                .AnyAsync(a => a.DoctorId == Appointment.DoctorId
                            && a.AppointmentDate > conflictStartTime
                            && a.AppointmentDate < conflictEndTime);

            if (hasConflict)
            {
                ModelState.AddModelError("", "The doctor is unavailable at this time. Please choose a time at least 1 hour apart from existing appointments.");
                return Page();
            }

            // 3. Patient Conflict Detection (Same patient, any doctor, within 1 hour)
            // User requirement: "patient is able to book the appointment for the same day at same time with different doctor it should not happen"
            bool patientHasConflict = await _context.Appointments
                .AnyAsync(a => a.PatientId == Appointment.PatientId
                            && a.AppointmentDate > conflictStartTime
                            && a.AppointmentDate < conflictEndTime);

            if (patientHasConflict)
            {
                ModelState.AddModelError("", "You already have an appointment scheduled around this time (possibly with another doctor). Please choose a different time.");
                return Page();
            }

            Appointment.Status = "Scheduled";
            Appointment.CreatedAt = DateTime.Now;

            _context.Appointments.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
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

        private async Task LoadPatientsAsync()
        {
            Patients = await _context.Patients
                .Select(p => new SelectListItem
                {
                    Value = p.PatientId.ToString(),
                    Text = p.FullName
                })
                .ToListAsync();
        }
    }
}
