namespace Api;

public class MyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IService _service;
    private readonly ILogger<MyMiddleware> _logger;

    public MyMiddleware(
        RequestDelegate next, 
        IService service, 
        ILogger<MyMiddleware> logger)
    {
        _next = next;
        _service = service;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Middleware: {Id}", _service.GetId());
        await _next(context);
    }
}
