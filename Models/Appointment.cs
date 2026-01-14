using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HCAMiniEHR.Validation;

namespace HCAMiniEHR.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        // Foreign Keys
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [AppointmentDateValidation]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Appointment Time")]
        [NotMapped]
        public TimeSpan AppointmentTime { get; set; }

        [StringLength(200)]
        public string? Reason { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;

        public ICollection<LabOrder> LabOrders { get; set; }
            = new List<LabOrder>();
    }
}
