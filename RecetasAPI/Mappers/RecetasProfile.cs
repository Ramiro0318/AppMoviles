using RecetasAPI.Models.DTOs;
using RecetasAPI.Models.Entities;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecetasAPI.Mappers
{
    public static class RecetasProfile
    {
        //Metodos de estensión, sirven para agregarle cosas a los métodos que ya existen
        public static int DoubleUp(this int x)
        {
            return x * 2;
        }
        public static CategoriaDTO ToCategoriaDTO(this Categorias categorias)
        {
            return new CategoriaDTO
            {
                Id = categorias.Id,
                Nombre = categorias.Nombre
            };
        }

        public static RecetaMenuDTO ToRecetaMenuDTO(this Recetas r)
        {
            return new RecetaMenuDTO
            {
                Id = r.DatabaseId ?? 0,
                ImagenUrl = r.ImagenUrl ?? "",
                Nombre = r.Titulo,
                Porcion = r.Porcion ?? "",
                Tiempo = r.Tiempo ?? "",
            };
        }

        public static RecetaDTO ToRecetaDTO(this Recetas r)
        {
            return new RecetaDTO
            {
                Nombre = r.Titulo,
                Descripcion = r.Descripcion ?? "",
                ImagenURL = r.ImagenUrl ?? "",
                Porciones = r.Porcion ?? "",
                Tiempo = r.Tiempo ?? "",
                Categoria = string.Join(", ", r.Categoria.Select(x => x.Nombre)),
                Ingredientes = r.Ingredientes.GroupBy(g => g.Seccion)
                .Select(x => new GrupoDTO
                    {
                        Grupo = x.Key ?? "",
                        Elementos = x.Select(e => new ElementosDTO
                        {
                            Orden = e.Orden ?? 0,
                            Nombre = e.IngredienteTexto
                        }).OrderBy(x => x.Orden).ToList()
                    }).ToList(),
                Procedimiento = r.Pasospreparacion.GroupBy(g => g.Seccion)
                .Select(x => new GrupoDTO
                {
                    Grupo = x.Key ?? "",
                    Elementos = x.Select(e => new ElementosDTO
                    {
                        Orden = e.NumeroPaso,
                        Nombre = e.Descripcion,
                    }).OrderBy(x => x.Orden).ToList()
                }).ToList(),
            };
        }
    }
}
