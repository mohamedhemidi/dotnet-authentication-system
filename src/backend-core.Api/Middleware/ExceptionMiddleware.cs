using System.Net;
using backend_core.Application.Common.Exceptions;
using FluentValidation;
using Newtonsoft.Json;


namespace backend_core.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

            var errors = ex.Errors.Select(e => new ValidationError
            {
                PropertyName = e.PropertyName,
                ErrorMessage = e.ErrorMessage,
            });

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails
            {
                status = HttpStatusCode.UnprocessableEntity,
                message = "Validation failed",
                errors = errors,
                stackTrace = ex.StackTrace
            }));
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResult = new ErrorDetails { message = exception.Message, stackTrace = exception.StackTrace };

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                errorResult.status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                errorResult.status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(UnauthorizedException))
            {
                errorResult.status = HttpStatusCode.Unauthorized;
            }
            else
            {
                errorResult.status = HttpStatusCode.InternalServerError;
            }

            var exceptionResult = JsonConvert.SerializeObject(errorResult);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorResult.status;

            return context.Response.WriteAsync(exceptionResult);
        }

    }

    public class ErrorDetails
    {
        public HttpStatusCode status { get; set; }
        public required string message { get; set; }
        public IEnumerable<ValidationError>? errors { get; set; }
        public required string stackTrace { get; set; }
    }
    public class ValidationError
    {
        public required string PropertyName { get; set; }
        public required string ErrorMessage { get; set; }
    }
}