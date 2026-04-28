using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Services;

namespace PendientesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService service;
        private readonly IValidator<LoginDTO> validator;

        public AuthController(AuthService service, IValidator<LoginDTO> validator)
        {
            this.service = service;
            this.validator = validator;
        }
        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            try
            {
                var validation = validator.Validate(dto);

                if (validation.IsValid)
                {
                    var token = service.Login(dto);
                    return Ok(token);
                }
                else
                {
                    return BadRequest(validation.Errors.Select(x => x.ErrorMessage));
                }
            }
            catch (KeyNotFoundException)
            {
                return Unauthorized();
            }
        }
    }
}
