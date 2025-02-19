using System.Linq.Expressions;
using Entities.Models;
using LiteNMS.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace LiteNMS.Infrastructure;

public class DeviceDiscoveryRepository : BaseRepository<DeviceDiscoveryResult>, IDeviceDiscoveryRepository
{
    private readonly ApplicationDbContext _context;

    public DeviceDiscoveryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<DeviceDiscoveryResult>> GetDiscoveredDevices(Guid deviceProvisionId)
    {
        return await _context.DeviceDiscoveryResults.Where(_ => _.DiscoveryProfileId == deviceProvisionId && _.Status == "Discoverable").ToListAsync();
    }
}
