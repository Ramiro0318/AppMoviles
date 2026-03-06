using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AsistenciasAPI.Models.DTOs
{
    public class GrupoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

    }

    public class AgregarGrupoDTO
    {
        public string Nombre { get; set; } = null!;
    }

    public class EditarGrupoDTO : GrupoDTO { }
}
