using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AutoMapper;

namespace AsistenciasAPI.Mappers
{
    public class GruposProfile : Profile
    {
        public GruposProfile()
        {
            CreateMap<Grupo, GrupoDTO>();
            CreateMap<AgregarGrupoDTO, Grupo>();
            CreateMap<EditarGrupoDTO, Grupo>();

        }
    }
}
