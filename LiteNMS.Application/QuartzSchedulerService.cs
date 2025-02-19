using LIteNMS.Application.Contracts;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
namespace LiteNMS.Services;

public class QuartzSchedulerService : IQuartzSchedulerService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;
    private IScheduler _scheduler;

    public QuartzSchedulerService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
    {
        _schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
        _jobFactory = jobFactory ?? throw new ArgumentNullException(nameof(jobFactory));
    }

    public async Task StartSchedulerAsync()
    {
        _scheduler = await _schedulerFactory.GetScheduler();
        _scheduler.JobFactory = _jobFactory;
        await _scheduler.Start();

        var jobDetail = JobBuilder.Create<MetricPollingJob>()
            .WithIdentity("GlobalMetricPolling", "GlobalMetricPolling")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("GlobalMetricPolling", "GlobalMetricPolling")
            .StartNow()
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
            .Build();

        await _scheduler.ScheduleJob(jobDetail, trigger);
    }

    public async Task StopSchedulerAsync()
    {
        if (_scheduler != null)
        {
            await _scheduler.Shutdown();
        }
    }
}