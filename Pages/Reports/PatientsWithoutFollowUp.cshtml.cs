using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCAMiniEHR.Data;
using HCAMiniEHR.DTOs;

namespace HCAMiniEHR.Pages.Reports
{
    public class PatientsWithoutFollowUpModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public PatientsWithoutFollowUpModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        public List<PatientNoFollowUpDTO> Patients { get; set; } = new();

        public async Task OnGetAsync()
        {
            var today = DateTime.Today;

            Patients = await _context.Patients
                .Where(p => !p.Appointments.Any(a => a.AppointmentDate >= today))
                .Select(p => new PatientNoFollowUpDTO
                {
                    PatientId = p.PatientId,
                    PatientName = p.FullName,
                    TotalAppointments = p.Appointments.Count
                })
                .OrderBy(p => p.PatientName)
                .ToListAsync();
        }
    }
}
