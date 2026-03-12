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
                y => y.MapFrom((o, d) => {
                    var estado = o.Asistencia.Where(x => x.Fecha == DateTime.Now)
                    .Select(x => x.IdEstado).FirstOrDefault();

                    return estado == 1 ? 'a' : estado == 2 ? 'B' : estado == 3 ? 'C';
                }))
                .ForMember(x => x.Justificacion, opt => opt.MapFrom((o,d) => {
                    var motivo = o.Asistencia.Where(x => x.Fecha == DateTime.Now)
                    .Select(x => x.IdEstado).FirstOrDefault();
                    return motivo;
                }
                ));
        }
    }
}
