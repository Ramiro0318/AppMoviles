using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Models.Validators;
using AsistenciasAPI.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AsistenciasAPI.Services
{
    public class AlumnosService
    {
        private readonly Repository<Alumno> repository;
        private readonly IMapper mapper;
        private readonly AgregarAlumnoValidator agregarValidator;
        private readonly EditarAlumnoValidator editarValidator;
        private readonly AsistenciaValidator asistenciaValidator;
        private readonly JustificacionValidator justificacionValidator;

        public AlumnosService(Repository<Alumno> repository, IMapper mapper, AgregarAlumnoValidator agregarValidator, EditarAlumnoValidator editarValidator, AsistenciaValidator asistenciaValidator, JustificacionValidator justificacionValidator) 
        {
            this.repository = repository;
            this.mapper = mapper;
            this.agregarValidator = agregarValidator;
            this.editarValidator = editarValidator;
            this.asistenciaValidator = asistenciaValidator;
            this.justificacionValidator = justificacionValidator;
        }

        public List<AlumnoDTO> GetByGrupos(int idGrupo, DateTime fecha) 
        {
            var alumnos = repository.Query()
                .Include(x => x.Asistencia.Where(x => x.Fecha == fecha.Date))
                .Where(x => x.IdGrupo == idGrupo);

            return alumnos.Select(x => mapper.Map<AlumnoDTO>(x)).ToList();
        }

        public AlumnoDetallesDTO GetById(int idAlumno) 
        {
            var alumno = repository.Get(idAlumno);

            if (alumno != null)
            {
                return mapper.Map<AlumnoDetallesDTO>(alumno);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public void AgregarAlumno(AgregarAlumnoDTO dto) 
        {
            var result = agregarValidator.Validate(dto);

            if (result.IsValid)
            {
                var entidad = mapper.Map<Alumno>(dto);
                repository.Insert(entidad);
            }
            else
            {
                throw new FluentValidation.ValidationException(result.Errors);
            }
        }

        public void AgregarAlumno(EditarAlumnoDTO dto) 
        {
            var result = editarValidator.Validate(dto);

            if (result.IsValid) 
            {
                var entidad = repository.Get(dto.Id);
                if (entidad == null)
                {
                    throw new KeyNotFoundException();
                }
                mapper.Map(dto, entidad);
                repository.Update(entidad);
            }
            else
            {
                throw new FluentValidation.ValidationException(result.Errors);
            }
        }


        public void EliminarAlumno(int id) 
        {
            if (repository.Get(id) == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                repository.Delete(id);
            }
        }




        public void RegistrarAsistencia(AsistenciaDTO dto) 
        {
            var result = asistenciaValidator.Validate(dto);

            if (result.IsValid) 
            {
                var entidad = repository.Get(dto.IdAlumno);

                if (entidad == null)
                {
                    throw new KeyNotFoundException();
                }
                else
                {
                    mapper.Map(dto, entidad);

                    var ola = repository.Query()
                        .Where(x => x.Id == dto.IdAlumno)
                        .Include(x => x.Asistencia
                        .Where(x => x.Fecha.Date.Date))
                        .Where(x => x.id == dto.IdAlumno).FirstPrDefault();

                    repository.Insert(entidad);
                }
            }
        }
    }
}
