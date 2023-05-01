using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using User.Domain.Exceptions;

namespace User.Api.Middlewares.ErrorHandling;

public static class ExceptionMiddleware
{
  public static void ExceptionHandler(HttpContext httpCtx)
  {
    var resp = httpCtx.Response;
    var exc = httpCtx.Features.Get<IExceptionHandlerFeature>()!.Error;

    // Determine HttpStatusCode by exception
    resp.StatusCode = exc switch
    {
      NotFoundException => StatusCodes.Status404NotFound,
      ValidationException => throw exc,
      _ => throw exc
    };
  }
}