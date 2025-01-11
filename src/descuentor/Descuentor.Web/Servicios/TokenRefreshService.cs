using Descuentor.Aplicacion.Dtos;

namespace Descuentor.Web.Servicios
{
    public class TokenRefreshService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;

        public TokenRefreshService(HttpClient httpClient, TokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<string?> GetValidAccessTokenAsync()
        {
            var expiration = await _tokenService.GetTokenExpirationAsync();
            if (DateTime.UtcNow >= expiration)
            {
                return await RefreshTokenAsync();
            }
            return await _tokenService.GetAccessTokenAsync();
        }

        private async Task<string> RefreshTokenAsync()
        {
            var refreshToken = await _tokenService.GetRefreshTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5095/identity/refresh", new { RefreshToken = refreshToken });

            if (response.IsSuccessStatusCode)
            {
                var tokens = await response.Content.ReadFromJsonAsync<TokenResponse>();
                await _tokenService.SetTokensAsync(tokens?.AccessToken!, tokens?.RefreshToken!, tokens.ExpiresIn);
                return tokens.AccessToken;
            }
            else
            {
                // Handle token refresh failure (e.g., redirect to login)
                return null;
            }
        }
    }
}