using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using ProEvents.Core.Exceptions;
using ProEvents.Core.Models;
using System.Text.Json;

namespace ProEvents.API.Extensions;

public static class ErrorHandlerExceptionExtensions
{
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (contextFeature != null)
                {
                    var error = GetError(contextFeature.Error);

                    context.Response.StatusCode = error.Status;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                }
            });
        });
    }

    private static Error GetError(Exception ex)
    {
        var exception = ex as ExceptionBase;

        if (exception?.Error != null)
            return exception.Error;

        return new Error
        {
            Status = StatusCodes.Status500InternalServerError,
            Message = ex.Message
        };
    }
}
