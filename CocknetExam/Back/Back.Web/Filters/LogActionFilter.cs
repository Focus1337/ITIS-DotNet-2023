using Microsoft.AspNetCore.Mvc.Filters;

namespace Back.Web.Filters;

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        switch (context.HttpContext.Request.Method)
        {
            case "GET":
                _logger.LogInformation($"GET request received: {context.HttpContext.Request.Path}");
                break;
            case "POST":
                _logger.LogInformation($"POST request received: {context.HttpContext.Request.Body}");
                break;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // do nothing
    }
}