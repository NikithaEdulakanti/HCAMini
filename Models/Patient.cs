using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace HCAMiniEHR.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Patient), nameof(ValidateDateOfBirth))]
        public DateOnly DateOfBirth { get; set; }

        // Custom DOB validation
        public static ValidationResult? ValidateDateOfBirth(DateOnly dob, ValidationContext context)
        {
            var minDate = new DateOnly(1900, 1, 1);
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (dob < minDate)
            {
                return new ValidationResult("Date of Birth cannot be before year 1900");
            }

            if (dob > today)
            {
                return new ValidationResult("Date of Birth cannot be in the future");
            }

            return ValidationResult.Success;
        }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
