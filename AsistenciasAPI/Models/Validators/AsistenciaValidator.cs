using AsistenciasAPI.Models.DTOs;
using FluentValidation;

namespace AsistenciasAPI.Models.Validators
{
    public class AsistenciaValidator : AbstractValidator<AsistenciaDTO>
    {
        public AsistenciaValidator()
        {
            RuleFor(x => x.IdAlumno).GreaterThan(0).WithMessage("Elija a un alumno");

            RuleFor(x => x.Fecha).LessThan(DateTime.Now.Date).WithMessage("La fecha no puede ser posterior a hoy");

        }
    }
}
