using HCAMiniEHR.Models;
using System.ComponentModel.DataAnnotations;

public class LabOrder
{
    public int LabOrderId { get; set; }

    [Required]
    public int AppointmentId { get; set; }

    [Required]
    public string TestName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime OrderedAt { get; set; } = DateTime.Now;

    public Appointment Appointment { get; set; } = null!;
}
