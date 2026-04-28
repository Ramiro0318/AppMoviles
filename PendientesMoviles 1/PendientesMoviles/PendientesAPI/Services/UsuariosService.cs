using AutoMapper;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;
using PendientesAPI.Validators;

namespace PendientesAPI.Services
{
    public class UsuariosService
    {
        private readonly Repository<Usuarios> repository;
        private readonly IMapper mapper;

        public UsuariosService(Repository<Usuarios> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void Registrar(UsuarioRequestDTO requestDTO)
        {
            var entidad = mapper.Map<Usuarios>(requestDTO);

            entidad.FechaCreacion = DateTime.Now;
            entidad.ContraseñaHash = Sha256Helper.ComputeHash(requestDTO.Contraseña);

            repository.Add(entidad);
            repository.SaveChanges();
        }



    }
}
