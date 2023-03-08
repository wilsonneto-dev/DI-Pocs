using Microsoft.Extensions.Logging;

namespace Console;

interface IApp
{
    void Run();
}

internal class App : IApp
{
    private readonly IService _service;
    private readonly ILogger<App> _logger;

    public App(IService service, ILogger<App> logger)
    {
        _service = service;
        _logger = logger;
    }

    public void Run() => 
        _logger.LogInformation(
            "Service id by Application => {Id}", 
            _service.GetId());
}
