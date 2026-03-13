using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Repositories;
using FluentValidation;

namespace AsistenciasAPI.Models.Validators
{
    public class EditarAlumnoValidator : AbstractValidator<EditarAlumnoDTO>
    {
        private readonly Repository<Alumno> repository;
        public EditarAlumnoValidator(Repository<Alumno> repository)
        {
            this.repository = repository;

            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Escriba el nombre del alumno")
                    .MaximumLength(20).WithMessage("Escriba un nombre de maximo 100 caracteres");


            RuleFor(x => x.NumControl).NotEmpty().WithMessage("Escriba el numero de control del alumno")
                    .MaximumLength(8).WithMessage("Escriba un nombre de maximo 8 caracteres")

            RuleFor(x => x).Must(NumControlRepetido).WithMessage("Ya existe un alumno con el mismo numero de control");

        }

        private bool NumControlRepetido(EditarAlumnoDTO dto)
        {
            return !repository.GetAll().Any(x => x.NumControl == dto.NumControl && x.Id != dto.Id);
        }
    }
}
