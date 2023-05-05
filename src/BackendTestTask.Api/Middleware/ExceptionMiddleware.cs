using System.Net;
using System.Text;
using BackendTestTask.Business.Interfaces;
using BackendTestTask.Business.Models;

namespace BackendTestTask.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IJournalService journalService)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, journalService);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IJournalService journalService)
    {
        var queryString = context.Request.QueryString.ToString();
        var requestBody = await GetRequestBody(context.Request);

        var headers = context.Request.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
        await journalService.SaveAsync(exception, queryString, requestBody, headers);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Type = exception.GetType().Name,
            Id = exception.HResult,
        }.ToString());
    }

    private static async Task<string> GetRequestBody(HttpRequest request)
    {
        if (!request.Body.CanSeek)
        {
            request.EnableBuffering();

        }
        request.Body.Position = 0;

        using var reader = new StreamReader(request.Body, Encoding.UTF8);
        var body = await reader.ReadToEndAsync().ConfigureAwait(false);
        request.Body.Position = 0;

        return body;
    }
}