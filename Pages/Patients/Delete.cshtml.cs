using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCAMiniEHR.Data;
using HCAMiniEHR.Models;

namespace HCAMiniEHR.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public DeleteModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Patient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (Patient == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return NotFound();

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
