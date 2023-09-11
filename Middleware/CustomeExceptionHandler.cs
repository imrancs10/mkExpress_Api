using Microsoft.AspNetCore.Builder;
using MKExpress.API.Dto.Response;
using System;

namespace MKExpress.API.Middleware
{

    public static class CustomExceptionHandler
    {
        private static ExceptionResponse? GetExceptionResponse(Exception exception)
        {
            return null;
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}