using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMemeber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (!customer.Birthdate.HasValue)
            {
                return new ValidationResult("Birthdate is Required");
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            if (age >= 18)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Customer must be at least 18 years old to go on a membership");
            }
        }
    }
}