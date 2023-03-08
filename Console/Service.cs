using Microsoft.Extensions.Logging;

namespace Console;

public interface IService : IDisposable
{
    string GetId();
    string GenerateNewId();
}
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
        _logger.LogInformation("{Hash} Creating the IdGenerator", GetHashCode());
    }

    public string CreateId()
    {
        _logger.LogInformation("{Hash} Creating Id", GetHashCode());
        return Random.Shared.Next(100, 999).ToString();
    }

    public void Dispose()
    {
        _logger.LogInformation("{Hash} disposing the IdGenerator", GetHashCode());
    }
}

public class Service : IService
{
    private readonly ILogger<Service> _logger;
    private readonly IIdGenerator _idGenerator;
    private string _id;

    public Service(
        ILogger<Service> logger,
        IIdGenerator idGenerator)
    {
        _logger = logger;
        _idGenerator = idGenerator;
        _id = _idGenerator.CreateId();
        _logger.LogInformation("{Id} Creating...", _id);
    }

    public void Dispose()
        => _logger.LogInformation("{Id} disposing", _id);
    public string GenerateNewId()
    {
        _logger.LogInformation("{Id} generating new id with idGenerator {hash}",
            _id, _idGenerator.GetHashCode());
        return _idGenerator.CreateId();
    }

    public string GetId() => _id;
}
