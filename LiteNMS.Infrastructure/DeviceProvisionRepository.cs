using Entities.Models;
using LiteNMS.Infrastructure.DBContexts;

namespace LiteNMS.Infrastructure;

public class DeviceProvisionRepository : BaseRepository<DeviceMetricProvision>, IDeviceProvisionRepository
{
    private readonly ApplicationDbContext _context;

    public DeviceProvisionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}