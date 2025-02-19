namespace LIteNMS.Application.Contracts;

public interface IQuartzSchedulerService
{
    Task StartSchedulerAsync();
    Task StopSchedulerAsync();
}