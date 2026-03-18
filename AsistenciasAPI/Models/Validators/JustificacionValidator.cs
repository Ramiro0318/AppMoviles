using AsistenciasAPI.Models.DTOs;
using FluentValidation;

namespace AsistenciasAPI.Models.Validators
{
    public class JustificacionValidator: AbstractValidator<JustificacionDTO>
    {
        public JustificacionValidator() 
        {
            RuleFor(x => x.IdAlumno).GreaterThan(0).WithMessage("Seleccione un alumno");

            RuleFor(x => x.Motivo).NotEmpty().WithMessage("Escriba un motivo")
                .MaximumLength(150).WithMessage("El motivo no debe exceder de 150 caracteres");
        }

    }
}
