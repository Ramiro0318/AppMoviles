using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PendientesAPI.DTOs;
using PendientesAPI.Services;
using System.Security.Claims;

namespace PendientesAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PendientesController : ControllerBase
{
    private readonly PendientesService _service;
    private readonly IValidator<PendienteRequestDTO> _validator;

    public PendientesController(PendientesService service, IValidator<PendienteRequestDTO> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet]
    public IActionResult GetByUser()
    {
        int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int idUsuario);

        var result = _service.GetByUser(idUsuario);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public IActionResult Create(PendienteRequestDTO dto)
    {
        int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int idUsuario);


        var validation = _validator.Validate(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

        var created = _service.Create(dto, idUsuario);
        return Ok(created.Id);
    }

    [HttpPut]
    public IActionResult Update(PendienteRequestDTO dto)
    {
        int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int idUsuario);


        var validation = _validator.Validate(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

        var updated = _service.Update(dto, idUsuario);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int idUsuario);

        var deleted = _service.Delete(id, idUsuario);
        return deleted ? Ok() : NotFound();
    }
}