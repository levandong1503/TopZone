using System.Net;

namespace TopZone.Middlewares;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // IMessageWriter is injected into InvokeAsync
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex) 
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var result = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An error occurred while processing your request.",
            Detailed = $"{exception.GetType().Name} - {exception.Message}", // You can include more detailed info if necessary
            StackTrace = exception.StackTrace,
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
    }
}
