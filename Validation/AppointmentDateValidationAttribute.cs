using System;
using System.ComponentModel.DataAnnotations;

namespace HCAMiniEHR.Validation
{
    public class AppointmentDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Appointment date is required.");
            }

            if (value is DateTime appointmentDate)
            {
                var today = DateTime.Today;
                var currentYear = DateTime.Now.Year;
                var appointmentYear = appointmentDate.Year;

                // Check if date is in the past
                if (appointmentDate.Date < today)
                {
                    return new ValidationResult("Appointment date cannot be in the past.");
                }

                // Check if date is beyond current year
                if (appointmentYear > currentYear)
                {
                    return new ValidationResult($"Appointment date cannot be beyond the current year ({currentYear}).");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}
