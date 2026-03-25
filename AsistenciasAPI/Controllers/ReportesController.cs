
using AsistenciaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        public ReportesController(ReporteService service)
        {
            Service = service;
        }

        public ReporteService Service { get; }

        [HttpGet("reporte/{idGrupo}/{fechaInicio}/{fechaFin}")]
        public async Task<IActionResult> Generar(int idGrupo, DateTime fechaInicio, DateTime fechaFin)

        {
            var pdf = await Service.GenerarReporteAsync(idGrupo, fechaInicio, fechaFin);

            return File(pdf, "application/pdf", $"Reporte_{idGrupo}.pdf");
        }
    }
}
