using System.Net.Http.Json;
using Descuentor.WebAssembly.Interfaces;
using Descuentor.WebAssembly.Modelos.Requests;
using Descuentor.WebAssembly.Modelos.Responses;

namespace Descuentor.WebAssembly.Servicios;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;
    private readonly string _baseUrl;

    public AuthService(HttpClient httpClient, ITokenService tokenService, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
        _baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value!;
    }

    public async Task<HttpResponseMessage> Register(RegisterRequest request)
    {
        Console.WriteLine("Request: " + request.ConfirmPassword);
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/usuarios", request);
        Console.WriteLine("Response: " + response);
        return response;
    }

    public async Task<TokenResponse> Login(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/identity/login", request);
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
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/identity/refresh", new RefreshTokenRequest { RefreshToken = refreshToken! });
        
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