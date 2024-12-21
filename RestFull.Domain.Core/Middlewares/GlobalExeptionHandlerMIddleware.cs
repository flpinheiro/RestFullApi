using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RestFull.Domain.Core.Exceptions;
using RestFull.Domain.Core.Helpers;

namespace RestFull.Domain.Core.Middlewares;

public class GlobalExeptionHandlerMIddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
		try
		{
			await _next(context);
		}
		catch(NotFoundException e) 
		{
			context.Response.StatusCode = 404;
			context.Response.Body = StreamHelper.GetStreamWithGetBytes(e.Message);
			return;
		}
		catch (Exception)
		{
			context.Response.StatusCode = 500;
			return;
		}
    }
}

public static class GlobalExeptionHandlerMIddlewareExtensions
{
    public static IApplicationBuilder UseGlobalException(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExeptionHandlerMIddleware>();
    }
}