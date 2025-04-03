using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Functions.Shared.DTOs.Auth;

namespace Functions.Client.Services
{
    public class AuthService(ILocalStorageService localStorage)
    {
        private const string TOKEN_KEY = "authToken";

        public async Task StoreAuthInfo(LoginResponseDTO loginResponse)
        {
            await localStorage.SetItemAsync(TOKEN_KEY, loginResponse.Token);
        }

        public async Task<string> GetToken()
        {
            return await localStorage.GetItemAsync<string>(TOKEN_KEY);
        }

        private ClaimsPrincipal? DecodeJwtToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                var claimsIdentity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                return new ClaimsPrincipal(claimsIdentity);
            }
            catch
            {
                return null; // Invalid token format
            }
        }

        public async Task<string?> GetUsername()
        {
            var token = await GetToken();
            var claims = DecodeJwtToken(token);
            return claims?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
        }

        public async Task<Guid?> GetUserId()
        {
            var token = await GetToken();
            var claims = DecodeJwtToken(token);
            var userIdClaim = claims?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();
            var claims = DecodeJwtToken(token);

            if (claims == null)
            {
                await Logout();
                return false;
            }

            var expClaim = claims.FindFirst("exp")?.Value;
            if (long.TryParse(expClaim, out long expUnixTime))
            {
                var expDate = DateTimeOffset.FromUnixTimeSeconds(expUnixTime).UtcDateTime;
                if (expDate < DateTime.UtcNow)
                {
                    await Logout();
                    return false; // Token expired
                }
            }

            return true;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync(TOKEN_KEY);
        }
    }
}
