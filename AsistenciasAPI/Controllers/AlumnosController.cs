using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly AlumnosService service;

        public AlumnosController(AlumnosService service)
        {
            this.service = service;
        }

        [HttpGet("grupo/{idgrrupo}/{fecha}")]
        public IActionResult Get(int idGrupo, DateTime fecha)
        {
            var grupo = service.GetByGrupos(idGrupo, fecha);

            if (grupo == null)
            {
                return NotFound();
            }
            return Ok(grupo);
        }

        [HttpGet("{idAlumno}")]
        public IActionResult GetAlumno(int idAlumno)
        {
            var alumno = service.GetById(idAlumno);

            if (alumno == null)
            {
                return NotFound();
            }
            return Ok(alumno);
        }

        [HttpPost]
        public IActionResult Post(AgregarAlumnoDTO dto)
        {
            try
            {
                service.AgregarAlumno(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Put(EditarAlumnoDTO dto)
        {
            try
            {
                service.EditarAlumno(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int idAlumno)
        {
            try
            {
                service.EliminarAlumno(idAlumno);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPut("asistio")]
        public IActionResult PutAsistio(AsistenciaDTO dto) 
        {
            try
            {
                service.RegistrarAsistencia(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("noasistio")]
        public IActionResult PutNoAsistio(AsistenciaDTO dto)
        {
            try
            {
                service.RegistrarAsistencia(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("justifico")]
        public IActionResult Justifico(JustificacionDTO dto)
        {
            try
            {
                service.JustificarInsistencia(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
