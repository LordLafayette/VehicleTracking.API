using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Application.Resources
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public string LicensePlate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public void Validate()
        {
            var validator = new RegisterRequestValidator();
            validator.ValidateAndThrow(this);
        }
    }

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(e => e.Username).NotEmpty();
            RuleFor(e => e.Password).NotEmpty();
            RuleFor(e => e.Email).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.LastName).NotEmpty();
            RuleFor(e => e.LicenseNumber).NotEmpty();
            RuleFor(e => e.LicensePlate).NotEmpty();
        }
    }
}
