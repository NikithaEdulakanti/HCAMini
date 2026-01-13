using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HCAMiniEHR.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Specialization { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        public ICollection<Appointment> Appointments { get; set; }
            = new List<Appointment>();
    }
}
