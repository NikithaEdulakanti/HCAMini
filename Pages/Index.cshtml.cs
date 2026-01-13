using HCAMiniEHR.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public IndexModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        public int TotalPatients { get; set; }
        public int TotalAppointments { get; set; }
        public int PendingLabOrders { get; set; }

        public async Task OnGetAsync()
        {
            TotalPatients = await _context.Patients.CountAsync();
            TotalAppointments = await _context.Appointments.CountAsync();
            PendingLabOrders = await _context.LabOrders
                .CountAsync(l => l.Status == "Pending");
        }
    }
}
