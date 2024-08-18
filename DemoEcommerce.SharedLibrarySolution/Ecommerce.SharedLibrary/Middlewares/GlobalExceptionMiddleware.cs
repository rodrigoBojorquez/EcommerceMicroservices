using System.Net;
using System.Text.Json;
using Ecommerce.SharedLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.SharedLibrary.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // variables
        string message = "Sorry, internal server error occurred, try again";
        int statusCode = StatusCodes.Status500InternalServerError;
        string title = "Error";

        try
        {
            await next(context);
            
            // check Too Many Request => 429
            if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
            {
                title = "Warning";
                message = "Too many requests";
                statusCode = StatusCodes.Status429TooManyRequests;
                await ModifyHeader(context, title, message, statusCode);
            }
            
            // check UnAuthorize => 401
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                title = "Alert";
                message = "Not authorized";
                statusCode = StatusCodes.Status401Unauthorized;
                await ModifyHeader(context, title, message, statusCode);
            }
            
            // check Forbidden => 403
            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                title = "Out of access";
                message = "Not allowed to access";
                statusCode = StatusCodes.Status403Forbidden;
                await ModifyHeader(context, title, message, statusCode);    
            }
        }
        catch (Exception ex)
        {
            // Log original exception
            LogException.LogExceptions(ex);
            
            // check Timeout => 408
            if (ex is TaskCanceledException || ex is TimeoutException)
            {
                message = "Request timeout, try again";
                title = "Time out";
                statusCode = StatusCodes.Status408RequestTimeout;
            }

            await ModifyHeader(context, title, message, statusCode);
        }
    }
    
    private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
        {
            Detail = message,
            Status = statusCode,
            Title = title,
        }), CancellationToken.None);
        return;
    }
}