namespace PendientesAPI.Models.DTOs
{
    public class UsuarioRequestDTO
    {
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string contrasena { get; set; } = null!;
    }
}
