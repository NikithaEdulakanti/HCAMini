using System;

namespace HCAMiniEHR.DTOs
{
    public class LabOrderDto
    {
        public int LabOrderId { get; set; }

        public string TestName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime OrderedAt { get; set; }
    }
}
