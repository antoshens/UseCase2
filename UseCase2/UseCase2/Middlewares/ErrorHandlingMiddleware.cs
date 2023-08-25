using Stripe;
using System.Net;
using System.Text.Json;

namespace UseCase2.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (StripeException stripeEx)
            {
                await HandleExceptionAsync(context, stripeEx, "Stripe API Error");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, "Internal Server Error");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, string description)
        {
            var code = HttpStatusCode.InternalServerError;

            _logger.LogError($"{description} - {exception.Message}");

            var result = JsonSerializer.Serialize(new
            {
                error = description,
                message = exception.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
