using AsistenciasAPI.Services;
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
    }
}
