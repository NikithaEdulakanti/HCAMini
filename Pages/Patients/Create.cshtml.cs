using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HCAMiniEHR.Data;
using HCAMiniEHR.Models;

namespace HCAMiniEHR.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public CreateModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Patients.Add(Patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }


    }
}
