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
            RuleFor(x => x.NombreUsuario).MinimumLength(3)
                .WithMessage("Escriba un nombre de usuario de mínimo 3 caracteres.")
                .MaximumLength(50).WithMessage("Escriba un nombre de usuario de máximo 50 caracteres.")
                .Must(ExisteNombre).WithMessage("El nombre de usuario no esta disponible")
                ;

            RuleFor(x => x.Correo).EmailAddress().WithMessage("Escriba un correo electrónico válido")
                .Must(ExisteCorreo).WithMessage("El correo electrónico ya esta registrado");

            RuleFor(x => x.Contraseña).MinimumLength(6)
                .WithMessage("Escriba una contraseña de al menos 6 caracteres");
            this.repository = repository;
        }


        bool ExisteCorreo(string correo)
        {
            return !repository.GetAll().Any(x=>x.Correo==correo);
        }

        bool ExisteNombre(string nombre)
        {
            return !repository.GetAll().Any(x => x.NombreUsuario == nombre);
        }
    }
}
