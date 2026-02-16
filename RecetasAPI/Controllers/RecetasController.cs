using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecetasAPI.Services;

namespace RecetasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetasController : ControllerBase
    {
        public readonly CategoriasService categoriasService;
        private readonly RecetasService recetasService;

        public RecetasController(CategoriasService categoriasService, RecetasService recetasService) 
        {
            this.categoriasService = categoriasService;
            this.recetasService = recetasService;
        }

        //Endpoint para categorias /api/recetas/categorias
        [HttpGet("categorias")]
        public IActionResult GetCategoria() 
        {
            var categoria = categoriasService.GetCategorias();
            return Ok(categoria);
        }


        //Endpoint para el menu de recetas /api/recetas/categorias/id
        [HttpGet("categorias/{id}")]
        public IActionResult GetIdCategoria(int id)
        {
            var recetas = recetasService.GetByCategoría(id);
            return Ok(recetas);
        }

        //Endpoint para receta /api/recetas/id

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var receta = recetasService.GetById(id);
            return Ok(receta);
        }
    }
}
