using Microsoft.EntityFrameworkCore;
using Repository.Contract;
using Repository.Contract.Models;

namespace Repository.Impl;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(LiteNmsDbContext context) : base(context) {}

    public async Task<User> GetByUsernameAsync(string username) =>
        await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
}