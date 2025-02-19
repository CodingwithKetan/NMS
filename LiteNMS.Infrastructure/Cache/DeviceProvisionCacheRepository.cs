using System.Collections.Concurrent;
using Entities.Models;

namespace LiteNMS.Infrastructure.Cache;

public class DeviceProvisionCacheRepository : IDeviceProvisionCacheRepository
    {
        private readonly ConcurrentDictionary<Guid, CacheEntry> _cache = new();
        private readonly IDeviceProvisionRepository _provisionRepository;

        public DeviceProvisionCacheRepository(IDeviceProvisionRepository provisionRepository)
        {
            var provisions = provisionRepository.GetAllAsync().Result;
            foreach (var provision in provisions)
            {
                AddOrUpdate(provision);
            }
        }


        public void AddOrUpdate(DeviceMetricProvision provision)
        {
            var now = DateTime.UtcNow;
            
            
            var nextPollingTime = now.AddSeconds(provision.PollTime - (now - provision.CreatedAt).TotalSeconds % provision.PollTime);

            _cache.AddOrUpdate(provision.Id,
                new CacheEntry(provision.Id, nextPollingTime, provision.PollTime),
                (key, existing) =>
                {
                    existing.NextPollingTime = nextPollingTime;
                    return existing;
                });
        }

        public void UpdateProvisionTime(Guid provisionId)
        {
            if (_cache.TryGetValue(provisionId, out var entry))
            {
                entry.LastProvisionTime = DateTime.UtcNow;
                entry.NextPollingTime = DateTime.UtcNow.AddSeconds(entry.PollTime);
            }
        }

        public List<Guid> GetDueProvisions()
        {
            var now = DateTime.UtcNow;
            return _cache.Values
                .Where(e => e.NextPollingTime <= now)
                .Select(e => e.ProvisionId)
                .ToList();
        }

        public void Remove(Guid provisionId)
        {
            _cache.TryRemove(provisionId, out _);
        }

        public bool IsEmpty() => !_cache.Any();
    }

    public class CacheEntry
    {
        public Guid ProvisionId { get; }
        public DateTime NextPollingTime { get; set; }
        public DateTime LastProvisionTime { get; set; }
        public int PollTime { get; set; }

        public CacheEntry(Guid provisionId, DateTime nextPollingTime, int pollTime)
        {
            ProvisionId = provisionId;
            NextPollingTime = nextPollingTime;
            LastProvisionTime = DateTime.MinValue;
            PollTime = pollTime;
        }
    }
