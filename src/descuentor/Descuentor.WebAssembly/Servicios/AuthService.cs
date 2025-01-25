using System.Net.Http.Json;
using Descuentor.WebAssembly.Interfaces;
using Descuentor.WebAssembly.Modelos.Requests;
using Descuentor.WebAssembly.Modelos.Responses;

namespace Descuentor.WebAssembly.Servicios;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;

    public AuthService(HttpClient httpClient, ITokenService tokenService)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    public async Task<HttpResponseMessage> Register(RegisterRequest request)
    {
        Console.WriteLine("Request: " + request.ConfirmPassword);
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5095/api/usuarios", request);
        Console.WriteLine("Response: " + response);
        return response;
    }

    public async Task<TokenResponse> Login(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5095/identity/login", request);
        Console.WriteLine("Response: " + response);
        
        if (response.IsSuccessStatusCode)
        {
            var authResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            
            // Guardar tokens en localStorage
            await _tokenService.SetTokensAsync(authResponse?.AccessToken!, authResponse?.RefreshToken!, authResponse.ExpiresIn);
            
            return authResponse;
        }
        
        throw new Exception("Error en el login");
    }
    
    public async Task<bool> RefreshTokenAsync()
    {
        var refreshToken = await _tokenService.GetRefreshTokenAsync();
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5095/identity/refresh", new RefreshTokenRequest { RefreshToken = refreshToken! });
        
        if (response.IsSuccessStatusCode)
        {
            var authResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            
            // Guardar tokens en localStorage
            await _tokenService.SetTokensAsync(authResponse?.AccessToken!, authResponse?.RefreshToken!, authResponse.ExpiresIn);
            
            return true;
        }
        else
        {
            return false;
        }
        
        throw new Exception("Error al refrescar el token");
    }
    
    public async Task Logout()
    {
        await _tokenService.RemoveTokensAsync();
    }
}