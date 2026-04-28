namespace PendientesAPI.Models.DTOs
{
    public class UsuarioRequestDTO
    {
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contraseña { get; set; } = null!;

    }
}
