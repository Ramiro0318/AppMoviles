using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PendientesAPI.Helper;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;
using PendientesAPI.Services;

namespace PendientesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private  UsuariosService service;
        private  IValidator<UsuariosController> validator;

        public UsuariosController( UsuariosService service, IValidator<UsuariosController> validator) 
        {
            this.service = service;
            this.validator = validator;

        }

        [HttpPost]
        public ActionResult Create(UsuarioRequestDTO requestDTO) 
        {
            var validacion = validator.Validate(requestDTO);
            if (!validacion.IsValid) 
            {
                return BadRequest(validacion.Errors.Select(x => x.ErrorMessage));
            }
            service.Regsitrar(requestDTO);
            return Ok();
         }
    }


}
