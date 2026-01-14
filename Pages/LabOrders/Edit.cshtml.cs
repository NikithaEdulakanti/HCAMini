using HCAMiniEHR.Data;
using HCAMiniEHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Pages.LabOrders
{
    public class EditModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public EditModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LabOrder LabOrder { get; set; } = new();

        public bool IsCompleted { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var labOrder = await _context.LabOrders
                .Include(l => l.Appointment)
                .ThenInclude(a => a.Patient)
                .FirstOrDefaultAsync(l => l.LabOrderId == id);

            if (labOrder == null)
                return NotFound();

            LabOrder = labOrder;
            IsCompleted = LabOrder.Status == "Completed";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Fetch original to verify rules
            var originalOrder = await _context.LabOrders.AsNoTracking()
                .FirstOrDefaultAsync(l => l.LabOrderId == LabOrder.LabOrderId);

            if (originalOrder == null)
                return NotFound();

            if (originalOrder.Status == "Completed")
            {
                // Strict Rule: If it was completed, you CANNOT change it.
                // Even if user hacks the form, we reject changes.
                // We could return an error, or just ignore (since UI is disabled).
                // Let's enforce strictly.
                if (LabOrder.Status != "Completed" || LabOrder.TestName != originalOrder.TestName)
                {
                    ModelState.AddModelError("", "Cannot modify a completed Lab Order.");
                    // Reload data for display
                    IsCompleted = true; 
                    // Need to reload related data for display
                     var reloaded = await _context.LabOrders
                        .Include(l => l.Appointment).ThenInclude(a => a.Patient)
                        .FirstOrDefaultAsync(l => l.LabOrderId == LabOrder.LabOrderId);
                     if(reloaded != null) LabOrder = reloaded;

                    return Page();
                }
            }

            _context.Attach(LabOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabOrderExists(LabOrder.LabOrderId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }

        private bool LabOrderExists(int id)
        {
            return _context.LabOrders.Any(e => e.LabOrderId == id);
        }
    }
}
