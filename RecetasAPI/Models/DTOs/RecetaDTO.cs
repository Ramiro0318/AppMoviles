namespace RecetasAPI.Models.DTOs
{
    public class RecetaDTO
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Porciones { get; set; } = null!;
        public string Tiempo { get; set; } = null!;
        public string ImagenURL { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public List<GrupoDTO>? Ingredientes { get; set; }
        public List<GrupoDTO>? Procedimiento { get; set; }

    }

    public class GrupoDTO
    {
        public string Grupo { get; set; } = null!;
        public List<ElementosDTO>? Elementos { get; set; }

    }

    public class ElementosDTO
    {
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;

    }
}
