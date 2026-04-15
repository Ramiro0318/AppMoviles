namespace PendientesAPI.DTOs;

public class PendienteResponseDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Estado { get; set; } = string.Empty;
}

public class PendienteRequestDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Estado { get; set; } = "Pendiente";
}