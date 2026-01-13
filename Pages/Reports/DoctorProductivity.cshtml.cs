using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCAMiniEHR.Data;
using HCAMiniEHR.DTOs;

namespace HCAMiniEHR.Pages.Reports
{
    public class DoctorProductivityModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public DoctorProductivityModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        public List<DoctorProductivityDTO> Report { get; set; } = new();

        public async Task OnGetAsync()
        {
            Report = await _context.Appointments
                .Include(a => a.Doctor)
                .GroupBy(a => a.Doctor.FullName)
                .Select(g => new DoctorProductivityDTO
                {
                    DoctorName = g.Key,
                    TotalAppointments = g.Count()
                })
                .OrderByDescending(d => d.TotalAppointments)
                .ToListAsync();
        }
    }
}
