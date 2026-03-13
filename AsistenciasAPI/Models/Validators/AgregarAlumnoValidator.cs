using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Repositories;
using FluentValidation;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AsistenciasAPI.Models.Validators
{
    public class AgregarAlumnoValidator : AbstractValidator<AgregarAlumnoDTO>
    {
        private readonly Repository<Alumno> repository;
        public AgregarAlumnoValidator(Repository<Alumno> repository)
        {
            this.repository = repository;

            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Escriba el nombre del alumno")
                    .MaximumLength(20).WithMessage("Escriba un nombre de maximo 100 caracteres");

            RuleFor(x => x.IdGrupo).GreaterThan(0).WithMessage("Indique el grupo al que pertenece el alumno");

            RuleFor(x => x.NumControl).NotEmpty().WithMessage("Escriba el numero de control del alumno")
                    .MaximumLength(8).WithMessage("Escriba un nombre de maximo 8 caracteres")
                    .Must(NumControlRepetido).WithMessage("El nombre del repositorio ya está registrado"); 

        }

        private bool NumControlRepetido(string numControl) 
        {
            return !repository.GetAll().Any(x => x.NumControl == numControl);
        }
    }
}
