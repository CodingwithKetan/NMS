using Repository.Contract.Models;

namespace Service.Contract;

public interface IAuthService
{
    string GenerateJwtToken(User user);
}