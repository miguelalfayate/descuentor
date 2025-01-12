using System.Net.Http.Headers;
using Descuentor.Web.Servicios;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Descuentor.Web.Proveedores
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly TokenService _tokenService;

        public CustomAuthenticationStateProvider(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _tokenService.GetAccessTokenAsync();
            
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync("http://localhost:5095/identity/manage/info");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<object>();
                Console.WriteLine("Response content: " + content);
                Console.WriteLine("Response content: " + content);

            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
            
            
            ClaimsIdentity identity;

            if (!string.IsNullOrEmpty(accessToken))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "miguel@domain.com"),
                    new Claim(ClaimTypes.NameIdentifier, "5")
                };

                identity = new ClaimsIdentity(claims, "apiauth_type");
                Console.WriteLine("Id Usuario: " + identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string userName)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName)
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}