using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecetasAPI.Mappers;
using RecetasAPI.Models.DTOs;
using RecetasAPI.Models.Entities;
using RecetasAPI.Repositories;

namespace RecetasAPI.Services
{
    public class CategoriasService
    {
        private readonly Repository<Categorias> repository;

        public CategoriasService(Repository<Categorias> repository)
        {
            this.repository = repository;
        }
        public int Doblar() 
        { 
            var x = 2;
            return x.DoubleUp();
        }

        public List<CategoriaDTO> GetCategorias()
        {
            var datos = repository.GetAll().OrderBy(x => x.Nombre);

            return datos.Select(x => x.ToCategoriaDTO()).ToList();
            //Uso de mapper
            //return datos.Select(x => new CategoriaDTO
            //{
            //    Id = x.Id,
            //    Nombre = x.Nombre
            //}).ToList();
        }




    }
}
