using FluentValidation;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;

namespace PendientesAPI.Validators
{
    public class UserValidator:AbstractValidator<UsuarioRequestDTO>
    {
        private readonly Repository<Usuarios> repository;

        public UserValidator(Repository<Usuarios> repository) 
        {
            RuleFor(x => x.NombreUsuario).MinimumLength(3).WithMessage("Escriba un nombre de mínimo 3 caracteres.")
                .MaximumLength(50).WithMessage("Escriba un nombre de máximo 50 caractes.")
                .Must(ExisteNombnre).WithMessage("El nombre de usuario no está disponible");
            

            RuleFor(x => x.Correo).EmailAddress().WithMessage("Escriba un nuevo correo electrónico.")
                .Must(ExisteCorreo).WithMessage("El correo ya está registrado.");

            RuleFor(x => x.contrasena).MinimumLength(6).WithMessage("Escriba una contraseña de al menos 6 caracteres.");
            this.repository = repository;
        }

        bool ExisteCorreo(string correo) 
        {
            return !repository.GetAll().Any(x => x.Correo == correo);
        }

        bool ExisteNombnre(string nombre)
        {
            return !repository.GetAll().Any(x => x.NombreUsuario == nombre);
        }

    }
}
