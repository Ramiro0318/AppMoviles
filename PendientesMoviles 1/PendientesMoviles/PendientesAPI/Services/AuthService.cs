using Microsoft.IdentityModel.Tokens;
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

        public TokenDTO Login(LoginDTO dto)
        {
            var hash = Sha256Helper.ComputeHash(dto.Password ?? "");

            var usuario = repository.GetAll().FirstOrDefault(x => x.NombreUsuario == dto.Username && x.ContraseñaHash == hash);


            if (usuario == null)
                throw new KeyNotFoundException();

            //Generar el JWT

            var claims = new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };

            var token = GenerarJWT(claims);
            var refreshToken = Guid.NewGuid().ToString();

            refreshToken nuevo = new refreshTokens()
            {
                idUsuario = usuario.Id,
                FechaVencimiento = DateTime.UtcNow.AddDays(1),
                token = token
            };

            refreshtokkenRepository.add(nuevo);
            refreshTokenRepository.saveChanges();


            return new TokenDTO { 
                RefreshToken = refreshToken,
                Token = token,
            };
        }

        public TokenDTO TokenRenewal(TokenRenovationDTO dto) 
        {
            var rt = repository.GetAll().FirstOrDefault(x => x.token == dto.RefreshToken);

            if (rt != null || rt.FechaVencimiento < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException();
            }

            var usuario = repository.GetAll().FirstOrDefault(x => x.NombreUsuario == dto.Username && x.ContraseñaHash == hash);


            if (usuario == null)
                throw new KeyNotFoundException();

            //Generar el JWT

            var claims = new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };

            var token = GenerarJWT(claims);
            var refreshToken = Guid.NewGuid().ToString();

            refreshToken nuevo = new refreshTokens()
            {
                idUsuario = usuario.Id,
                FechaVencimiento = DateTime.UtcNow.AddDays(1),
                token = token
            };

            refreshtokkenRepository.add(nuevo);

            refreshTokenRepository.delete(rt);
            refreshTokenRepository.saveChanges();

            return new TokenDTO
            {
                RefreshToken = refreshToken,
                Token = token,
            };

        }

        private string GenerarJWT(List<Claim> claims)
        {
            var key = configuration.GetValue<string>("Jwt:SecretKey");

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key??"")), SecurityAlgorithms.HmacSha256 ) 
                );

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(tokenDescriptor);
        }
    }
}
