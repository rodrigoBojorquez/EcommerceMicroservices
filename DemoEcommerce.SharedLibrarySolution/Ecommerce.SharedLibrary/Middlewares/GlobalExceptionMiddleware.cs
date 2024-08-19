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
        try
        {
            await next(context);

            // check Too Many Requests => 429
            if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
            {
                await ModifyHeader(context, "Warning", "Too many requests", StatusCodes.Status429TooManyRequests);
            }
            // check Unauthorized => 401
            else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                await ModifyHeader(context, "Alert", "Not authorized", StatusCodes.Status401Unauthorized);
            }
            // check Forbidden => 403
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                await ModifyHeader(context, "Out of access", "Not allowed to access", StatusCodes.Status403Forbidden);
            }
        }
        catch (Exception ex)
        {
            // Log original exception
            LogException.LogExceptions(ex);

            // Check Timeout => 408
            if (ex is TaskCanceledException || ex is TimeoutException)
            {
                await ModifyHeader(context, "Time out", "Request timeout, try again", StatusCodes.Status408RequestTimeout);
                return; // Ensure the response is not modified further
            }

            // If not handled, fallback to 500
            await ModifyHeader(context, "Error", "Sorry, internal server error occurred, try again", StatusCodes.Status500InternalServerError);
        }
    }
    
    private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
    {
        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
        {
            Detail = message,
            Status = statusCode,
            Title = title,
        }), CancellationToken.None);
    }
}