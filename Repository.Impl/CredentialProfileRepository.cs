using Microsoft.EntityFrameworkCore;
using Repository.Contract;
using Repository.Contract.Models;

namespace Repository.Impl;

public class CredentialProfileRepository : Repository<CredentialProfile>, ICredentialProfileRepository
{
    private readonly LiteNmsDbContext _context;

    public CredentialProfileRepository(LiteNmsDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CredentialProfile?> GetByNameAsync(string name)
    {
        return await _context.CredentialProfiles.FirstOrDefaultAsync(c => c.Name == name);
    }
}
