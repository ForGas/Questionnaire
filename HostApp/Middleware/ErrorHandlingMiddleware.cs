using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.Common.Exceptions;

namespace HostApp.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly bool _includeDetails;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment env
        )
    {
        _next = next;
        _logger = logger;
       _includeDetails = !env.IsProduction();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception error)
    {
        HttpResponse response = context.Response;
        response.ContentType = "application/problem+json";
        var problem = new ProblemDetails();

        switch (error)
        {
            case ValidationException or BadRequestException or ArgumentNullException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                problem.Status = (int)HttpStatusCode.BadRequest;
                problem.Title = error.Message;
                problem.Detail = error.Message;
                break;
            case KeyNotFoundException or EntityNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                problem.Status = (int)HttpStatusCode.NotFound;
                problem.Title = error.Message;
                problem.Detail = error.Message;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problem.Status = (int)HttpStatusCode.InternalServerError;
                problem.Title = error.Message;
                problem.Detail = error.Message;
                break;
        }

        if (_includeDetails)
        {
            problem.Detail = error.ToString();
        }

        var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;

        if (traceId != null)
        {
            problem.Extensions["traceId"] = traceId;
        }

        await response.WriteAsJsonAsync(problem);
    }
}
