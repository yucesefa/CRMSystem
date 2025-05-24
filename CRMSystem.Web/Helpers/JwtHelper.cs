using System.IdentityModel.Tokens.Jwt;

namespace CRMSystem.Web.Helpers
{
    public class JwtHelper
    {
        public static string GetRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role" || c.Type.EndsWith("/role"));
            return roleClaim?.Value;
        }
    }
}
