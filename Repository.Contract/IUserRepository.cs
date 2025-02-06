using Repository.Contract.Models;

namespace Repository.Contract;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
}