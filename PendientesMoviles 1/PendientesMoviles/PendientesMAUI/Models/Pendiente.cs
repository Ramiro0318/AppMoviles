namespace PendientesMAUI.Models;

public class Pendiente
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Estado { get; set; } = "Pendiente";

    public string EstadoIcono => Estado == "Completado" ? "✅" : "⏳";
}