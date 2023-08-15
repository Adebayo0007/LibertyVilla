using Models;
using System.Security.Claims;

namespace LibertyVilla_WebApi.Authentication
{
    public interface IJWTAuthentication
    {
        string GenerateToken(LoginResponseModel model);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
