using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace AsistenciasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoControllers : Controller
    {
        public GruposService Service { get; }
        public GrupoControllers(GruposService service)
        {
            Service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var dto = Service.GetAll();
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dto = Service.Get(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Post(AgregarGrupoDTO dto) 
        {
            try
            {
                var id = Service.Agregar(dto);
                return Created("grupo", id); //Cuando no es rest hacer un post que a veces haga esto, a veces otra cosa
            }
            catch (ValidationException ex)
            {
                var mensajesError = ex.Errors.Select(x => x.ErrorMessage);
                return BadRequest(mensajesError);
            }
        
        }

        [HttpPut]
        public IActionResult Put(EditarGrupoDTO dto)
        {
            try
            {
                Service.Editar(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                var mensajesError = ex.Errors.Select(x => x.ErrorMessage);
                return BadRequest(mensajesError);
            }

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            if (Service.Eliminar(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
