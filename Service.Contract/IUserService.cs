using Repository.Contract.Models;

namespace Service.Contract;

public interface IUserService
{
    Task<User> RegisterAsync(string username, string password);
    Task<string> AuthenticateAsync(string username, string password);
}