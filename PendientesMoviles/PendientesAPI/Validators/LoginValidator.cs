using FluentValidation;
using PendientesAPI.Models.DTOs;

namespace PendientesAPI.Validators
{
    public class LoginValidator:AbstractValidator<LoginDTO>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Escriba el nombre del usuario");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Escriba la contraseña");
        }
    }
}
