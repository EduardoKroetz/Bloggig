using Bloggig.Application.DTOs;
using Microsoft.AspNetCore.Diagnostics;

namespace Bloggig.Presentation.Exception;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = 400;
        switch (exception)
        {
            case ArgumentException:
                statusCode = 400;
                break;
            case UnauthorizedAccessException: 
                statusCode = 401;
                break;
            default:
                statusCode = 500;
                break;

        }

        var response = ResultDto.BadResult(exception.Message);

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
