using AsistenciaAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AutoMapper;

public class ReporteProfile : Profile
{
    public ReporteProfile()
    {
        //CreateMap<Estado, string>()
        //    .ConvertUsing(src =>
        //        src.Id == 1 ? "✓" :
        //        src.Id == 2 ? "X" :
        //        src.Id == 3 ? "J" : "");

        CreateMap<Asistencia, FechaDTO>()
              .ForMember(d => d.Estado, o => o.MapFrom(src =>
        src.IdEstado == 1 ? "A" :
        src.IdEstado == 2 ? "F" :
        src.IdEstado == 3 ? "J" : " "
    ))
            .ForMember(d => d.Fecha, o => o.MapFrom(s => s.Fecha));

        CreateMap<Alumno, AlumnoFechaDTO>()
            .ForMember(d => d.NumCtrl, o => o.MapFrom(s => s.NumControl))
            .ForMember(d => d.Nombre, o => o.MapFrom(s => s.Nombre))
            .ForMember(d => d.ListaFecha, o => o.MapFrom(s => s.Asistencia));

        CreateMap<Grupo, ReporteDTO>()
            .ForMember(d => d.Nombre, o => o.MapFrom(s => s.Nombre))
            .ForMember(d => d.ListaAlumnos, o => o.MapFrom(s => s.Alumno));
    }
}