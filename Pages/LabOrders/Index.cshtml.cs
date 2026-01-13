using HCAMiniEHR.Data;
using HCAMiniEHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Pages.LabOrders
{
    public class IndexModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public IndexModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        public IList<LabOrder> LabOrders { get; set; } = new List<LabOrder>();

        public int? PatientId { get; set; }
        public string? PatientName { get; set; }

        public async Task OnGetAsync(int? patientId)
        {
            PatientId = patientId;

            var query = _context.LabOrders
                .Include(l => l.Appointment)
                    .ThenInclude(a => a.Patient)
                .AsQueryable();

            //  FILTER BY PATIENT
            if (patientId.HasValue)
            {
                query = query.Where(l => l.Appointment.PatientId == patientId);

                PatientName = await _context.Patients
                    .Where(p => p.PatientId == patientId)
                    .Select(p => p.FullName)
                    .FirstOrDefaultAsync();
            }

            LabOrders = await query
                .OrderByDescending(l => l.OrderedAt)
                .ToListAsync();
        }
    }
}
