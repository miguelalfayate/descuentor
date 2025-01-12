using System.Net.Http.Json;
using Blazored.LocalStorage;
using Descuentor.Aplicacion.Dtos;
using Descuentor.Aplicacion.Interfaces;
using Descuentor.Web.Servicios;

namespace Descuentor.WebAssembly.Servicios;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly TokenService _tokenService;
    private readonly ILocalStorageService _localStorage;
    
    public AuthService(HttpClient httpClient, ILocalStorageService localStorage, TokenService tokenService)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _tokenService = tokenService;
    }

    public async Task<bool> Register(RegisterRequest request)
    {
        Console.WriteLine("Request: " + request.ConfirmPassword);
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5095/identity/register", request);
        Console.WriteLine("Response: " + response);
        return response.IsSuccessStatusCode;
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
            
            //await _localStorage.SetItemAsync("accessToken", authResponse?.AccessToken);
            //await _localStorage.SetItemAsync("refreshToken", authResponse?.RefreshToken);
            
            return authResponse;
        }
        
        throw new Exception("Error en el login");
    }
}