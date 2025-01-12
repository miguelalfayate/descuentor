using Descuentor.Aplicacion.Dtos;

namespace Descuentor.Aplicacion.Interfaces;

public interface IAuthService
{
    Task<bool> Register(RegisterRequest request);
    Task<TokenResponse> Login(LoginRequest request);
}