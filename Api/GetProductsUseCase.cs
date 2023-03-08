using System.Runtime.CompilerServices;

namespace Api;

public interface IGetProductsUseCase
{
    void Execute();
}

public class GetProductsUseCase : IGetProductsUseCase
{
    private readonly ILogger<GetProductsUseCase> _logger;
    private readonly IService _service;
    private readonly INameService<GetProductsUseCase> _nameService;

    public GetProductsUseCase(
        ILogger<GetProductsUseCase> logger,
        IService service,
        INameService<GetProductsUseCase> nameService)
    {
        _logger = logger;
        _service = service;
        _nameService = nameService;
    }

    public void Execute()
    {
        // _logger.LogInformation("use case executing");
        // _logger.LogInformation("Service id: {Id}", _service.GetId());
        // _logger.LogInformation("UseCase name: {Name}", _nameService.GetName(this));
        _service.GenerateNewId();
    }
}
