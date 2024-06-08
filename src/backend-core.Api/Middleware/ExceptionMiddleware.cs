using System.Net;
using backend_core.Application.Common.Exceptions;
using backend_core.Domain.Common;
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

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiResponse<IEnumerable<ValidationError>>
            {
                IsSuccess = false,
                Message = "Validation Failed",
                StatusCode = (int)HttpStatusCode.UnprocessableEntity,
                Response = errors,
                StackTrace = ex.StackTrace!
            }));
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorResult = new ApiResponse<Exception>
            {
                IsSuccess = false,
                Message = ex.Message,
                StackTrace = ex.StackTrace!,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                errorResult.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(UnauthorizedException))
            {
                errorResult.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var exceptionResult = JsonConvert.SerializeObject(errorResult);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResult.StatusCode;

            return context.Response.WriteAsync(exceptionResult);
        }

    }

    public class ValidationError
    {
        public required string PropertyName { get; set; }
        public required string ErrorMessage { get; set; }
    }
}