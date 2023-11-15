namespace OpenWeather.WebAPI.Middlewares
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
