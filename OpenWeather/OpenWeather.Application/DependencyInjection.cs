namespace OpenWeather.Application;

using OpenWeather.Domain.Interfaces;
using OpenWeather.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddOpenWeatherDependencies(this IServiceCollection services)
    {
        services.AddTransient<IWeatherRepository, WeatherRepository>();
        services.AddHttpClient<IWeatherRepository, WeatherRepository>();

        services.AddTransient<IGeocodingRepository, GeocodingRepository>();
        services.AddHttpClient<IGeocodingRepository, GeocodingRepository>();
    }
}