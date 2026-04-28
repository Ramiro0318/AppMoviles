using AutoMapper;
using PendientesAPI.DTOs;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;

namespace PendientesAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pendientes, PendienteResponseDTO>();
        CreateMap<PendienteRequestDTO, Pendientes>();
        CreateMap<UsuarioRequestDTO, Usuarios>();
    }
}