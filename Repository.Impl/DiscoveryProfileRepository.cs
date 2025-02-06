using Microsoft.EntityFrameworkCore;
using Repository.Contract;
using Repository.Contract.Models;

namespace Repository.Impl;

public class DiscoveryProfileRepository : Repository<DiscoveryProfile>, IDiscoveryProfileRepository
{
    private readonly LiteNmsDbContext _context;

    public DiscoveryProfileRepository(LiteNmsDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DiscoveryProfile?> GetByNameAsync(string name)
    {
        return await _context.DiscoveryProfiles.FirstOrDefaultAsync(p => p.Name == name);
    }
}
