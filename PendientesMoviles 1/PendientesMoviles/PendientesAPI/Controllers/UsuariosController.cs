using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PendientesAPI.DTOs;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace PendientesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosService service;
        private readonly IValidator<UsuarioRequestDTO> validator;

        public UsuariosController(UsuariosService service, IValidator<UsuarioRequestDTO> validator)
        {
            this.service = service;
            this.validator = validator;
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioRequestDTO dto)
        {
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            service.Registrar(dto);
            return Ok();
        }
    }
}
