namespace OpenWeather.Application;

using OpenWeather.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using OpenWeather.Application.DataProviders;

public static class DependencyInjection
{
    public static void AddOpenWeather(this IServiceCollection services)
    {
        services.AddTransient<IWeatherDataProvider, WeatherDataProvider>();
        services.AddHttpClient<IWeatherDataProvider, WeatherDataProvider>();

        services.AddTransient<IGeocodingDataProvider, GeocodingDataProvider>();
        services.AddHttpClient<IGeocodingDataProvider, GeocodingDataProvider>();
    }
}