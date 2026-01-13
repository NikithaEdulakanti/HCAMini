using System;

namespace HCAMiniEHR.DTOs
{
    public class PendingLabOrderDTO
    {
        public int LabOrderId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public DateTime OrderedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
