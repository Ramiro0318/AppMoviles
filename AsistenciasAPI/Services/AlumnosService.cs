using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Models.Validators;
using AsistenciasAPI.Repositories;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq.Expressions;

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
        private readonly GruposService gruposService;

        public AlumnosService(Repository<Alumno> repository, IMapper mapper, AgregarAlumnoValidator agregarValidator, EditarAlumnoValidator editarValidator, AsistenciaValidator asistenciaValidator, JustificacionValidator justificacionValidator, GruposService gruposService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.agregarValidator = agregarValidator;
            this.editarValidator = editarValidator;
            this.asistenciaValidator = asistenciaValidator;
            this.justificacionValidator = justificacionValidator;
            this.gruposService = gruposService;
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
                if (gruposService.Get(dto.IdGrupo) == null)
                {
                    result.Errors.Add(new ("IdGrupo","El grupo no existe."));
                    throw new ValidationException(result.Errors);
                }
                var entidad = mapper.Map<Alumno>(dto);
                repository.Insert(entidad);
            }
            else
            {
                throw new FluentValidation.ValidationException(result.Errors);
            }
        }

        public void EditarAlumno(EditarAlumnoDTO dto)
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
                var alumno = repository.Query()
                .Include(x => x.Asistencia.Where(x => x.Fecha.Date == dto.Fecha.Date))
                .Where(x => x.Id == dto.IdAlumno).FirstOrDefault();

                if (alumno == null)
                {
                    throw new KeyNotFoundException();
                }
                var asistencia = alumno.Asistencia.FirstOrDefault();
               
                if (asistencia != null)
                {
                    //Si ya hay asistencia es replace
                    asistencia.IdEstado = 1;
                    asistencia.Motivo = null;

                }
                else
                {
                    //Sino es insert
                    var EntidadAsistencia = mapper.Map<Asistencia>(dto);
                    EntidadAsistencia.IdEstado = 1;
                    alumno.Asistencia.Add(EntidadAsistencia);
                }
                repository.Update(alumno);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }


        public void RegistrarInsistencia(AsistenciaDTO dto)
        {
            var result = asistenciaValidator.Validate(dto);

            if (result.IsValid)
            {
                var alumno = repository.Query()
                .Include(x => x.Asistencia.Where(x => x.Fecha.Date == dto.Fecha.Date))
                .Where(x => x.Id == dto.IdAlumno).FirstOrDefault();

                if (alumno == null)
                {
                    throw new KeyNotFoundException();
                }
                var asistencia = alumno.Asistencia.FirstOrDefault();

                if (asistencia != null)
                {
                    asistencia.IdEstado = 2;
                    asistencia.Motivo = null;
                }
                else
                {
                    var EntidadAsistencia = mapper.Map<Asistencia>(dto);
                    EntidadAsistencia.IdEstado = 2;
                    alumno.Asistencia.Add(EntidadAsistencia);
                }
                repository.Update(alumno);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public void JustificarInsistencia(JustificacionDTO dto)
        {
            var result = justificacionValidator.Validate(dto);

            if (result.IsValid)
            {
                var alumno = repository.Query()
                .Include(x => x.Asistencia.Where(x => x.Fecha.Date == dto.Fecha.Date))
                .Where(x => x.Id == dto.IdAlumno).FirstOrDefault();

                if (alumno == null)
                {
                    throw new KeyNotFoundException();
                }
                var asistencia = alumno.Asistencia.FirstOrDefault();

                if (asistencia != null)
                {
                    asistencia.IdEstado = 3;
                    asistencia.Motivo = dto.Motivo;
                }
                else
                {
                    var EntidadAsistencia = mapper.Map<Asistencia>(dto);
                    EntidadAsistencia.IdEstado = 3;
                    alumno.Asistencia.Add(EntidadAsistencia);
                }
                repository.Update(alumno);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

    }
}
