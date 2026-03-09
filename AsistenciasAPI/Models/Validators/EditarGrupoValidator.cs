using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Repositories;
using FluentValidation;

namespace AsistenciasAPI.Models.Validators
{
    public class EditarGrupoValidator:AbstractValidator<EditarGrupoDTO>
    {
        private readonly Repository<Grupo> repository;
        public EditarGrupoValidator(Repository<Grupo> repository)
        {
            this.repository = repository;

            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Escriba el nombre del grupo")
                .MaximumLength(20).WithMessage("El grupo no puede tener mas de 20 caracteres de largo");

            RuleFor(x => x)
                .Must(ExisteGrupo)
                .WithMessage("El nombre del repositorio ya está registrado");

        }

        public bool ExisteGrupo(EditarGrupoDTO grupo) 
        {
            return !repository.GetAll().Any(x => grupo.Id != x.Id && grupo.Nombre == x.Nombre);
        }
    }
}
