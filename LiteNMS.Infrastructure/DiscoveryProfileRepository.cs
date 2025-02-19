using Entities.Models;
using LiteNMS.Infrastructure.DBContexts;

namespace LiteNMS.Infrastructure;

public class DiscoveryProfileRepository : BaseRepository<DiscoveryProfile>, IDiscoveryProfileRepository
{
    public DiscoveryProfileRepository(ApplicationDbContext context) : base(context) { }
}