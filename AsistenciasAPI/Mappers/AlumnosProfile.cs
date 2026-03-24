using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AutoMapper;

namespace AsistenciasAPI.Mappers
{
    public class AlumnosProfile : Profile
    {
        public AlumnosProfile()
        {
            CreateMap<Alumno, AlumnoDTO>()
                .ForMember(x => x.Asistencia,
                y => y.MapFrom((o, d) =>
                {
                    var estado = o.Asistencia.Where(x => x.Fecha == DateTime.Now.Date)
                    .Select(x => x.IdEstado).FirstOrDefault();

                    return estado == 1 ? 'A' : estado == 2 ? 'F' : estado == 3 ? 'J' : ' ';
                }))
                .ForMember(x => x.Justificacion, opt => opt.MapFrom((o, d) =>
                {
                    var motivo = o.Asistencia.Where(x => x.Fecha == DateTime.Now.Date)
                    .Select(x => x.IdEstado).FirstOrDefault();
                    return motivo;
                }

                ));

            CreateMap<AgregarAlumnoDTO, Alumno>();
            CreateMap<Alumno, AlumnoDetallesDTO>();
            CreateMap<EditarAlumnoDTO, Alumno>();
            CreateMap<AsistenciaDTO, Asistencia>();
            CreateMap<JustificacionDTO, Asistencia>();
        }


    }
}
