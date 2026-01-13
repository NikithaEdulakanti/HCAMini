namespace HCAMiniEHR.DTOs
{
    public class PatientNoFollowUpDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int TotalAppointments { get; set; }
    }
}
