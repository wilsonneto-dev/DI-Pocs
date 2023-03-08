using Microsoft.AspNetCore.Mvc.Filters;

namespace Api;

public class LogFilter : ActionFilterAttribute
{
    private readonly ILogger<LogFilter> _logger;

    public LogFilter(ILogger<LogFilter> logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Before executing");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("After executing");
    }
}
