using PendientesAPI.Helper;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;
using System.Security.Claims;

namespace PendientesAPI.Services
{
    public class AuthService
    {
        private readonly Repository<Usuarios> repository;
        private readonly IConfiguration configuration;

        public AuthService(Repository<Usuarios> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }
        public string Login(LoginDTO dto)
        {
            var hash = EncriptacionHelper.Encrypt(dto.Password ?? "");

            var usuarios = repository.GetAll().FirstOrDefault(x => x.NombreUsuario == dto.Username && x.ContraseñaHash == hash);

            if (usuarios == null)
            {
                throw new KeyNotFoundException();
            }
            //Generar el JWT

            return ""; GenerarJWT();
        }

        public string GenerarJWT(List<Claim> claims) 
        {
        
        }
    }
}
