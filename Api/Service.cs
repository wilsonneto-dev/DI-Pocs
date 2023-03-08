using Microsoft.Extensions.Logging;

namespace Api;

public interface IService : IDisposable
{
    string GetId();
    string GenerateNewId();
}

public interface IServiceSingleton : IService { };
public interface IServiceScoped : IService { };

public interface IIdGenerator : IDisposable
{
    string CreateId();
}

public class IdGenerator : IIdGenerator
{
    private readonly ILogger<IdGenerator> _logger;

    public IdGenerator(ILogger<IdGenerator> logger)
    {
        _logger = logger;
        // _logger.LogInformation("{Hash} Creating the IdGenerator", GetHashCode());
    }

    public string CreateId()
    {
        // _logger.LogInformation("{Hash} Creating Id", GetHashCode());
        return Random.Shared.Next(100,999).ToString();
    }

    public void Dispose()
    {
        // _logger.LogInformation("{Hash} disposing the IdGenerator", GetHashCode());
    }
}

public class Service : IService
{
    private readonly ILogger<Service> _logger;
    private readonly IIdGenerator _idGenrator;
    private string _id;

    public Service(
        ILogger<Service> logger,
        IIdGenerator idGenerator)
    {
        _logger = logger;
        _idGenrator = idGenerator;
        _id = _idGenrator.CreateId();
        _logger.LogInformation("{Id} Creating...", _id);
    }

    public void Dispose()
        => _logger.LogInformation("{Id} disposing", _id);
    public string GenerateNewId()
    {
        _logger.LogInformation("{Id} generating new id with idGenerator {hash}", 
            _id, _idGenrator.GetHashCode());
        return _idGenrator.CreateId();
    }

    public string GetId() => _id;
}

public class ServiceSingleton : Service, IServiceSingleton
{
    public ServiceSingleton(ILogger<Service> logger, IIdGenerator idGenerator)
        : base(logger, idGenerator)
    {
    }
}

public class ServiceScoped : Service, IServiceScoped
{
    public ServiceScoped(ILogger<Service> logger, IIdGenerator idGenerator) 
        : base(logger, idGenerator)
    {
    }
}