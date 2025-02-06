using LiteNMS.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Repository.Contract;
using Repository.Contract.Models;

namespace Repository.Impl;

public class DiscoveryRepository : Repository<Discovery>, IDiscoveryRepository
{
    private readonly LiteNmsDbContext _context;

    public DiscoveryRepository(LiteNmsDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Discovery>> FilterDiscoveriesAsync(DiscoveryFilterDto filter)
    {
        var query = _context.Discoveries.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Status))
            query = query.Where(d => d.Status == filter.Status);

        if (!string.IsNullOrEmpty(filter.Subnet))
            query = query.Where(d => d.ProfileId == filter.ProfileId);

        if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            query = query.Where(d => d.StartedAt >= filter.StartDate && d.StartedAt <= filter.EndDate);

        return await query.ToListAsync();
    }
}
