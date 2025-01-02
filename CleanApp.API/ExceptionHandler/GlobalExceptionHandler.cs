using App.Application;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CleanApp.API.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorAsDto = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);
            await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken);
            return true;
        }
    }
}
