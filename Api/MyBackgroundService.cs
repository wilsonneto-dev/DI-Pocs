using Microsoft.Extensions.DependencyInjection;

namespace Api;

public class MyBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public MyBackgroundService(IServiceScopeFactory serviceScopeFactory) 
        => _scopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var useCase = scope.ServiceProvider
                    .GetRequiredService<IProcessPaymentUseCase>();
                useCase.Execute();
            }
            await Task.Delay(1000);
        }
    }
}

public interface IProcessPaymentUseCase
{
    void Execute();
}

public class ProcessPaymentUseCase : IProcessPaymentUseCase
{
    private readonly ILogger<ProcessPaymentUseCase> _logger;
    private readonly IService _service;
    private readonly IServiceSingleton _serviceSingleton;
    private readonly IServiceScoped _serviceScoped;

    public ProcessPaymentUseCase(
        ILogger<ProcessPaymentUseCase> logger,
        IService service,
        IServiceSingleton serviceSingleton,
        IServiceScoped serviceScoped)
    {
        _logger = logger;
        _service = service;
        _serviceSingleton = serviceSingleton;
        _serviceScoped = serviceScoped;
    }

    public void Execute() => _logger.LogInformation(
        "My service is running here... " +
        "t:{Id} s:{IdSingleton} sc:{IdScoped}",
        _service.GetId(),
        _serviceSingleton.GetId(),
        _serviceScoped.GetId());
}
