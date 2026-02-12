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

        public List<CategoriaDTO> GetCategorias() {
            
        
        }

    }
}
