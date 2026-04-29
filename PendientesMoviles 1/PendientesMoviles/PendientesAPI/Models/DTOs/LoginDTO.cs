namespace PendientesAPI.Models.DTOs
{
    public class LoginDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }    
    }

    public class TokenDTO
    {
        public string? UserName { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class TokenRenovationDTO
    {
        public string RefreshToken { get; set; } = null!;
    }
}
