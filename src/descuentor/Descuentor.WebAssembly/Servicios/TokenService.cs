using Blazored.LocalStorage;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Descuentor.Web.Servicios
{
    public class TokenService
    {
        private readonly ILocalStorageService _localStorage;

        public TokenService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SetTokensAsync(string accessToken, string refreshToken, int expiresIn)
        {
            await _localStorage.SetItemAsStringAsync("accessToken", accessToken);
            await _localStorage.SetItemAsStringAsync("refreshToken", refreshToken);
            await _localStorage.SetItemAsStringAsync("tokenExpiration", expiresIn.ToString());
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            var token = "";
            try
            {
                token = await _localStorage.GetItemAsStringAsync("accessToken"); //The exception is thrown at this line
            }
            catch (InvalidOperationException)
            {
            }

            Console.WriteLine(token);
            return token;

            // return await _localStorage.GetItemAsStringAsync("accessToken");
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            return await _localStorage.GetItemAsStringAsync("refreshToken");
        }

        public async Task<DateTime> GetTokenExpirationAsync()
        {
            var expiresIn = await _localStorage.GetItemAsStringAsync("tokenExpiration");
            var expirationParsed = int.Parse(expiresIn!);
            var expiration = DateTime.UtcNow.AddSeconds(expirationParsed);
            return expiration;
        }

        public async Task RemoveTokensAsync()
        {
            await _localStorage.RemoveItemAsync("accessToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("tokenExpiration");
        }
    }
}