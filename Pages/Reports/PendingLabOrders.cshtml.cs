using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCAMiniEHR.Data;
using HCAMiniEHR.DTOs;

namespace HCAMiniEHR.Pages.Reports
{
    public class PendingLabOrdersModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public PendingLabOrdersModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        public List<PendingLabOrderDTO> PendingLabOrders { get; set; } = new();

        public async Task OnGetAsync()
        {
            PendingLabOrders = await _context.LabOrders
                .Include(l => l.Appointment)
                    .ThenInclude(a => a.Patient)
                .Where(l => l.Status == "Pending")
                .Select(l => new PendingLabOrderDTO
                {
                    LabOrderId = l.LabOrderId,
                    PatientName = l.Appointment.Patient.FullName,
                    TestName = l.TestName,
                    OrderedAt = l.OrderedAt,
                    Status = l.Status
                })
                .OrderBy(l => l.OrderedAt)
                .ToListAsync();
        }
    }
}
