using Descuentor.Aplicacion.Dtos;

namespace Descuentor.Aplicacion.Interfaces;

public interface IAuthService
{
    Task<HttpResponseMessage> Register(RegisterRequest request);
    Task<TokenResponse> Login(LoginRequest request);
    Task<bool> RefreshTokenAsync();
    Task Logout();
}