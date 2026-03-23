using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Repositories;
using FluentValidation;

namespace AsistenciasAPI.Models.Validators
{
    public class AgregarGrupoValidator: AbstractValidator<AgregarGrupoDTO> 
    {
        private readonly Repository<Grupo> repository;
        public AgregarGrupoValidator(Repository<Grupo> repository) 
        {
            this.repository = repository;
        
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Escriba el nombre del grupo")
                .MaximumLength(20).WithMessage("El grupo no puede tener mas de 20 caracteres de largo")
                .Must(nombre => !repository.GetAll().Any(g => g.Nombre == nombre))
                .WithMessage("El nombre del repositorio ya está registrado");

        }

    }
}
