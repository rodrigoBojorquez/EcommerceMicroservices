using Microsoft.AspNetCore.Http;

namespace Ecommerce.SharedLibrary.Middlewares;

public class ListenToOnlyApiGateway(RequestDelegate next)
{
    /*
     * Checar que todas las requests vienen del API gateway
     *
     * NULL significa que la peticion no viene del api gateway
     */
    public async Task InvokeAsync(HttpContext context)
    {
        var signedHeader = context.Request.Headers["Api-Gateway"];

        if (signedHeader.FirstOrDefault() is null)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Service is unavailable");
            return;
        }
        else
        {
            await next(context);
        }
    }
}