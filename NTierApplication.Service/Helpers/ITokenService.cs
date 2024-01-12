using NTierApplication.DataAccess.Models;

namespace NTierApplication.Service.Helpers;

public interface ITokenService
{
    public string GenerateToken(User user);
}
