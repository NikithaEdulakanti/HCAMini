using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCAMiniEHR.Data;
using HCAMiniEHR.DTOs;

namespace HCAMiniEHR.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly HCAMiniEHRDbContext _context;

        public IndexModel(HCAMiniEHRDbContext context)
        {
            _context = context;
        }

        public IList<PatientSummaryDTO> Patients { get; set; } = new List<PatientSummaryDTO>();

        public async Task OnGetAsync()
        {
            Patients = await _context.Patients
                .OrderBy(p => p.FullName)
                .Select(p => new PatientSummaryDTO
                {
                    PatientId = p.PatientId,
                    FullName = p.FullName,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth
                })
                .ToListAsync();
        }
    }
}
