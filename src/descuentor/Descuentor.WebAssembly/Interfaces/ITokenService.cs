namespace Descuentor.WebAssembly.Interfaces;

public interface ITokenService
{
    Task SetTokensAsync(string accessToken, string refreshToken, int expiresIn);
    Task<string?> GetAccessTokenAsync();
    Task<string?> GetRefreshTokenAsync();
    Task<DateTime> GetTokenExpirationAsync();
    Task RemoveTokensAsync();
}