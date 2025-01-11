using Blazored.LocalStorage;

namespace Descuentor.Web.Servicios
{
    using Microsoft.JSInterop;
    using System.Threading.Tasks;

    public class TokenService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILocalStorageService _localStorage;

        public TokenService(IJSRuntime jsRuntime, ILocalStorageService localStorage)
        {
            _jsRuntime = jsRuntime;
            _localStorage = localStorage;
        }

        public async Task SetTokensAsync(string accessToken, string refreshToken, int expiresIn)
        {
            
            await _localStorage.SetItemAsStringAsync("accessToken", accessToken);
            await _localStorage.SetItemAsStringAsync("refreshToken", refreshToken);
            await _localStorage.SetItemAsStringAsync("tokenExpiration", expiresIn.ToString());

            // await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", accessToken);
            // await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", refreshToken);
            // await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "tokenExpiration", expiresIn.ToString());
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            return await _localStorage.GetItemAsStringAsync("accessToken");
            // return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accessToken");
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            // return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "refreshToken");
            return await _localStorage.GetItemAsStringAsync("refreshToken");
        }

        public async Task<DateTime> GetTokenExpirationAsync()
        {
            //var expiration = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "tokenExpiration");
            var expiresIn = await _localStorage.GetItemAsStringAsync("tokenExpiration");
            var expirationParsed = int.Parse(expiresIn!);
            var expiration = DateTime.UtcNow.AddSeconds(expirationParsed);
            return expiration;
        }

        public async Task RemoveTokensAsync()
        {
            // await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
            // await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");
            // await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "tokenExpiration");
            
            await _localStorage.RemoveItemAsync("accessToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("tokenExpiration");
        }
    }
}