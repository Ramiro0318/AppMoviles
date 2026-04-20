using Microsoft.IdentityModel.Tokens;
using PendientesAPI.Helper;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, usuarios.Id.ToString()), new Claim(ClaimTypes.Name, usuarios.NombreUsuario)
            };
            return "";
        }

        public string GenerarJWT(List<Claim> claims)
        {
            var key = configuration.GetValue<string>("Jwt:SecretKey");

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key?? "")), SecurityAlgorithms.HmacSha256)
                );

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(tokenDescriptor);
        }
    }
}
