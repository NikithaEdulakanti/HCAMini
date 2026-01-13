using System;

namespace HCAMiniEHR.DTOs
{
    public class AppointmentSummaryDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public int TotalAppointments { get; set; }

        public DateTime? LastAppointmentDate { get; set; }

        public int UpcomingAppointments { get; set; }
    }
}
