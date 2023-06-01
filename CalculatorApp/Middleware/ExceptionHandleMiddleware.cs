using System.Net;
using CalculatorApp.Models;

namespace CalculatorApp.Middleware;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext,
        IWebHostEnvironment environment)
    {
        httpContext.Request.EnableBuffering();

        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(
        HttpContext httpContext,
        Exception ex)
    {
        var responseErrorMessage = ex.Message;

        var errorResponse = new { Error = responseErrorMessage };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var mes = new Result<Object>(new Error()
        {
            Message = responseErrorMessage,
            Code = (int)HttpStatusCode.BadRequest
        });
        

        return httpContext.Response.WriteAsJsonAsync(mes);
        
        
    }
}