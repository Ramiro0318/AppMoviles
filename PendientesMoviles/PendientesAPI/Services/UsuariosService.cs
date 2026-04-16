using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PendientesAPI.Helper;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;

namespace PendientesAPI.Services
{
    public class UsuariosService
    {
        private readonly Repository<Usuarios> repository;
        private readonly IMapper mapper;

        public UsuariosService(Repository<Usuarios> repository, IMapper mapper) 
        {

            this.repository = repository;
            this.mapper = mapper;
        }


        public void Regsitrar(UsuarioRequestDTO requestDTO) 
        {
            var entidad = mapper.Map<Usuarios>(requestDTO);

            entidad.FechaCreacion = DateTime.Now;
            entidad.ContraseñaHash = EncriptacionHelper.Encrypt(requestDTO.contrasena);

            repository.Add(entidad);
            repository.SaveChanges();
        }
    }
}
