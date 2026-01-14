using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace HCAMiniEHR.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        [Required]
        [MaxLength(100)]

        [RegularExpression("^[a-zA-Z]+$",ErrorMessage = "Name can contain letters.")]
        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

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

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", 
            ErrorMessage = "Phone number must be in valid format (e.g., 123-456-7890, (123) 456-7890, or 1234567890)")]
        public string? Phone { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
