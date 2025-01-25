using Descuentor.WebAssembly.Modelos.Requests;
using Descuentor.WebAssembly.Modelos.Responses;

namespace Descuentor.WebAssembly.Interfaces;

public interface IAuthService
{
    Task<HttpResponseMessage> Register(RegisterRequest request);
    Task<TokenResponse> Login(LoginRequest request);
    Task<bool> RefreshTokenAsync();
    Task Logout();
}