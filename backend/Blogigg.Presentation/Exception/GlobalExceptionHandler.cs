using Bloggig.Application.DTOs;
using Microsoft.AspNetCore.Diagnostics;

namespace Bloggig.Presentation.Exception;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = 400;
        var message = "Ocorreu um erro ao processar a requisição";
        switch (exception)
        {
            case ArgumentException:
                statusCode = 400;
                break;
            case UnauthorizedAccessException: 
                statusCode = 401;
                message = "Você não está autorizado a acessar esse recurso";
                break;
            default:
                statusCode = 500;
                message = "Ocorreu um erro interno do servidor ao processar a requisição";
                break;

        }

        var response = ResultDto.BadResult(message);

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
