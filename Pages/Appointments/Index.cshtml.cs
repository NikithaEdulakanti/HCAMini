using HCAMiniEHR.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public IndexModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        public List<AppointmentVM> Appointments { get; set; } = new();

        public string? PatientName { get; set; }
        public int? PatientId { get; set; }

        public async Task OnGetAsync(int? patientId)
        {
            PatientId = patientId;

            var query = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .AsQueryable();

            if (patientId.HasValue)
            {
                query = query.Where(a => a.PatientId == patientId);

                PatientName = await _context.Patients
                    .Where(p => p.PatientId == patientId)
                    .Select(p => p.FullName)
                    .FirstOrDefaultAsync();
            }

            Appointments = await query
                .OrderBy(a => a.AppointmentDate)
                .Select(a => new AppointmentVM
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDate = a.AppointmentDate,
                    DoctorName = a.Doctor.FullName,
                    PatientName = a.Patient.FullName,
                    PatientId = a.PatientId,
                    Reason = a.Reason,
                    Status = a.Status
                })
                .ToListAsync();
        }

        public class AppointmentVM
        {
            public int AppointmentId { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string DoctorName { get; set; } = "";
            public string PatientName { get; set; } = "";
            public int PatientId { get; set; }
            public string Reason { get; set; } = "";
            public string Status { get; set; } = "";
        }
    }
}
