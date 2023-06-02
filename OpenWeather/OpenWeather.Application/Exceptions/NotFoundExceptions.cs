namespace OpenWeather.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class CityNotFoundException : NotFoundException
    {
        public CityNotFoundException() : base("City not found") { }
    }

    public class CountryNotFoundException : NotFoundException
    {
        public CountryNotFoundException() : base("This country does not exist") { }
    }
}
