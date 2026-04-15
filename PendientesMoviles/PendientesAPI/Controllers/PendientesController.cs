using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PendientesAPI.DTOs;
using PendientesAPI.Services;

namespace PendientesAPI.Controllers;

[ApiController]
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
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
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
        var validation = _validator.Validate(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

        var created = _service.Create(dto);
        return Ok(created.Id);
    }

    [HttpPut]
    public IActionResult Update(PendienteRequestDTO dto)
    {
        var validation = _validator.Validate(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

        var updated = _service.Update(dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        return deleted ? Ok() : NotFound();
    }
}