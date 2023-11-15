using OpenWeather.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace OpenWeather.WebAPI.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var response = string.Empty;

            switch (exception)
            {
                case NotFoundException notFoundEx:
                    code = HttpStatusCode.NotFound;
                    break;

                case ArgumentOutOfRangeException argOutEx:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;

            if (response == string.Empty)
                response = JsonSerializer.Serialize(new { error = exception.Message });

            return httpContext.Response.WriteAsync(response);
        }
    }
}
