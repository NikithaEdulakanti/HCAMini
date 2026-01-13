using HCAMiniEHR.Data;
using HCAMiniEHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Pages.LabOrders
{
    public class CreateModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public CreateModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        // ===== Bind LabOrder =====
        [BindProperty]
        public LabOrder LabOrder { get; set; } = new();

        // ===== Dropdown =====
        public SelectList AppointmentList { get; set; } = null!;

        // =========================
        // GET
        // =========================
        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAppointmentsAsync();
            return Page();
        }

        // =========================
        // POST
        // =========================
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(" OnPostAsync HIT");

            await LoadAppointmentsAsync();

            if (!ModelState.IsValid)
            {
                Console.WriteLine(" ModelState INVALID");

                foreach (var error in ModelState)
                {
                    foreach (var e in error.Value.Errors)
                    {
                        Console.WriteLine($"{error.Key} -> {e.ErrorMessage}");
                    }
                }

                return Page();
            }

            Console.WriteLine($"AppointmentId = {LabOrder.AppointmentId}");
            Console.WriteLine($"TestName = {LabOrder.TestName}");
            Console.WriteLine($"Status = {LabOrder.Status}");

            _context.LabOrders.Add(LabOrder);
            await _context.SaveChangesAsync();

            Console.WriteLine(" LabOrder Saved");

            return RedirectToPage("Index");
        }


        // =========================
        // Helper
        // =========================
        private async Task LoadAppointmentsAsync()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Select(a => new
                {
                    a.AppointmentId,
                    Display = $"#{a.AppointmentId} - {a.Patient.FullName}"
                })
                .ToListAsync();

            AppointmentList = new SelectList(appointments, "AppointmentId", "Display");
        }
    }
}
