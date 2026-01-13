using System;

namespace HCAMiniEHR.DTOs
{
    public class PatientSummaryDTO
    {
        public int PatientId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
    }
}
