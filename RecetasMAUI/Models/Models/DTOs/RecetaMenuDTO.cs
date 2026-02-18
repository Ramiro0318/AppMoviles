using System.Diagnostics.Contracts;

namespace RecetasAPI.Models.DTOs
{
    public class RecetaMenuDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Porcion { get; set; } = null!;
        public string Tiempo { get; set; } = null!;
        public string ImagenUrl { get; set; } = null!;
    }
}
