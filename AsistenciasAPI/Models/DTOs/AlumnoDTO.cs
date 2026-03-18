namespace AsistenciasAPI.Models.DTOs
{
    public class AlumnoDTO
    {
        public int Id { set; get; }
        public string Nombre { get; set; } = null!;
        public string NumControl { get; set; } = null!;
        public char Asistencia { get; set; }
        public string? Justificacion { get; set; }
    }

    public class AgregarAlumnoDTO 
    {
        public string Nombre { get; set; } = null!;
        public string NumControl { get; set; } = null!;
        public int IdGrupo { get; set; }
    }

    public class AlumnoDetallesDTO
    {
        public int Id { set; get; }
        public string Nombre { get; set; } = null!;
        public string NumControl { get; set; } = null!;
    }

    public class EditarAlumnoDTO : AlumnoDetallesDTO { }

    public class AsistenciaDTO 
    {
        public int IdAlumno { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class JustificacionDTO 
    {
        public int IdAlumno { get; set; }
        public string Motivo { set; get; } = null!;
    }
}
