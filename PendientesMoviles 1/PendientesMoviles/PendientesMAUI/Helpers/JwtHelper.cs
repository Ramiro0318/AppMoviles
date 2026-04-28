using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public static class JwtHelper
{
    public static string? GetClaimFromToken(string token, string claimType)
    {
        if (string.IsNullOrEmpty(token))
            return null;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value;
        }
        catch
        {
            return null;
        }
    }

    public static string? GetNameIdentifier(string token)
    {
        return GetClaimFromToken(token, ClaimTypes.NameIdentifier);
    }

    public static string? GetName(string token)
    {
        return GetClaimFromToken(token, ClaimTypes.Name);
    }
}