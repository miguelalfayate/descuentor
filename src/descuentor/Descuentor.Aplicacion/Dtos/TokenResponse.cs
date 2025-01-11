namespace Descuentor.Aplicacion.Dtos;

public class TokenResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    
    public int ExpiresIn { get; set; }
}