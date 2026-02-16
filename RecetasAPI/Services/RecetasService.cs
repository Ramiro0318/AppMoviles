using Microsoft.EntityFrameworkCore;
using RecetasAPI.Mappers;
using RecetasAPI.Models.DTOs;
using RecetasAPI.Models.Entities;
using RecetasAPI.Repositories;

namespace RecetasAPI.Services
{
    public class RecetasService
    {
        public Repository<Recetas> Repository { get; }
        public RecetasService(Repository<Recetas> repository)
        {
            Repository = repository;
        }

        public List<RecetaMenuDTO> GetByCategoría(int idCategoria) 
        {
            var receta = Repository.Query().Include(x => x.Categoria).Where(x => x.Categoria.Any(y => y.Id == idCategoria))
                
                .OrderBy(x => x.Titulo);

            return receta.Select(X => X.ToRecetaMenuDTO()).ToList();
        }

        public RecetaDTO? GetById(int id) 
        {
            var receta = Repository.Query()
                .Include(x => x.Categoria)
                .Include(x => x.Pasospreparacion)
                .Include(x => x.Ingredientes)
                .Where(x => x.DatabaseId == id).FirstOrDefault();

            return receta?.ToRecetaDTO();
        }

    }
}
