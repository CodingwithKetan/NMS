using LiteNMS.Infrastructure;
using LiteNMS.Infrastructure.Cache;
using Quartz;

namespace LiteNMS.Services;

public class MetricPollingJob : IJob
{
    private readonly IDeviceProvisionRepository _repository;
    private readonly IDeviceProvisionCacheRepository _cacheRepository;

    public MetricPollingJob(IDeviceProvisionRepository repository, IDeviceProvisionCacheRepository cacheRepository)
    {
        _repository = repository;
        _cacheRepository = cacheRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {

            var dueProvisions = _cacheRepository.GetDueProvisions();
            foreach (var provisionId in dueProvisions)
            {
                _cacheRepository.UpdateProvisionTime(provisionId);
            }
            Console.WriteLine($"Due Provisions: {dueProvisions.Count()}");
        }
        catch (Exception ex)
        {
            
        }
    }
}
