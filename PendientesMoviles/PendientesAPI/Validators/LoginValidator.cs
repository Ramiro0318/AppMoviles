using FluentValidation;
using PendientesAPI.Models.DTOs;

namespace PendientesAPI.Validators
{
    public class LoginValidator:AbstractValidator<LoginDTO>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.)
        }
    }
}
