namespace AsistenciaAPI.Models.DTOs
{
    public class ReporteDTO
    {
        public string Nombre { get; set; } = null!; // NombreGrupo
        public List<DateTime> Fechas { get; set; } = new();
        public List<AlumnoFechaDTO> ListaAlumnos { get; set; } = new();
    }

    public class AlumnoFechaDTO
    {
        public string NumCtrl { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public List<FechaDTO> ListaFecha { get; set; } = new();
    }

    public class FechaDTO
    {
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = null!; // ✓ X J
    }
}
