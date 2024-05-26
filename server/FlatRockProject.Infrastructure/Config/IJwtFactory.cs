using System.Security.Claims;

namespace FlatRockProject.Infrastructure.Config
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(string userId, string email, IEnumerable<Claim> additionalClaims);
    }
}
