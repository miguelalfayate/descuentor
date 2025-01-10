using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.JSInterop;

namespace Descuentor.Web.Autenticacion;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private const string AccessTokenKey = "accessToken";
    private const string RefreshTokenKey = "refreshToken";

    public CustomAuthStateProvider(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
    
        try
        {
            // Obtener el token directamente del localStorage del navegador
            var token = await GetStoredToken();

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Configurar el token para futuras peticiones
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);

            var handler = new JsonWebTokenHandler();
            var jwtToken = handler.ReadJsonWebToken(token);

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                // Token expirado, intentar renovar
                var refreshed = await TryRefreshToken();
                if (!refreshed)
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
                // Obtener el nuevo token después de refrescar
                token = await GetStoredToken();
                jwtToken = handler.ReadJsonWebToken(token);
            }

            var identity = new ClaimsIdentity(jwtToken.Claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
    
    private async Task<string> GetStoredToken()
    {
        var accessToken = await GetFromLocalStorage(AccessTokenKey);
        return accessToken;
    }

    private async Task<string> GetFromLocalStorage(string key)
    {
        //var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        var token = await _jsRuntime.InvokeAsync<string>("chrome.storage.local.get", key);
        await _jsRuntime.InvokeVoidAsync("logToConsole", $"Valor del token: {token}");

        return token;
    }

    private async Task<bool> TryRefreshToken()
    {
        try
        {
            var refreshToken = await GetFromLocalStorage(RefreshTokenKey);
            if (string.IsNullOrEmpty(refreshToken))
                return false;

            var response = await _httpClient.PostAsJsonAsync("identity/refresh", new
            {
                RefreshToken = refreshToken
            });

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task CheckAndUpdateAuthenticationState()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}