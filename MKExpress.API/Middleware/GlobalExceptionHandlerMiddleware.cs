#nullable enable
using Microsoft.EntityFrameworkCore;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Logger;
using System.Net;
using System.Text.Json;

namespace MKExpress.API.Middleware
{

    public class GlobalExceptionHandlerMiddleware
    {
        private readonly Func<Exception, ExceptionResponse>? _localExceptionHandlerFunc;
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _loggerManager;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next,ILoggerManager loggerManager,
            Func<Exception, ExceptionResponse>? localExceptionHandlerFunc = null)
        {
            _next = next;
            _localExceptionHandlerFunc = localExceptionHandlerFunc;
            _loggerManager = loggerManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
               //_loggerManager.LogError(ex, ex.Message); // Logging the global exception;
                await HandleExecptionAsync(context, ex);
            }
        }

        private async Task HandleExecptionAsync(HttpContext context, Exception exception)
        {
            // SeriLog Log exception in logger
            var response = context.Response;
            response.ContentType = "application/json";
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message
            };

            var exceptionResponse = _localExceptionHandlerFunc != null
                ? _localExceptionHandlerFunc(exception)
                : null;

            if (exceptionResponse != null)
            {
                response.StatusCode = exceptionResponse.StatusCode;
                errorResponse = exceptionResponse.ErrorResponse;
            }
            else
            {
                switch (exception)
                {
                    case BusinessRuleViolationException businessRuleViolationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.ErrorResponseType = businessRuleViolationException.ErrorResponseType;
                        break;
                    case NotFoundException notFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorResponse.ErrorResponseType = notFoundException.ErrorResponseType;
                        break;
                    case UnauthorizedException _:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        errorResponse.ErrorResponseType = "Unauthorized";
                        break;
                    case UnprocessableEntityException _:
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        errorResponse.ErrorResponseType = "UnprocessableEntity";
                        break;
                    case ForbiddenException _:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.ErrorResponseType = "Forbidden";
                        break;
                    case CustomValidationException validationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.ErrorResponseType = validationException.ErrorResponseType;
                        errorResponse.Errors = validationException.Errors;
                        break;
                    case HttpRequestException requestException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.ErrorResponseType = "HttpClientError";
                        errorResponse.Message = requestException.Message;
                        break;
                    case ServiceUnavailableException unavailableException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.ErrorResponseType = "ServiceUnavailable";
                        errorResponse.Message = unavailableException.HealthReportStatus;
                        break;
                    case DbUpdateException _:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.ErrorResponseType = "DatabaseUpdateError";
                        errorResponse.Message = exception.Message + "####" + exception.InnerException?.Message;
                        break;
                }

                var text = JsonSerializer.Serialize(errorResponse);
                await response.WriteAsync(text);
            }
        }
    }
}