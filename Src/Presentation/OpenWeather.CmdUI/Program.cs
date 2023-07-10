using OpenWeather.Application;
using OpenWeather.CmdUI.DTOs;
using System.Text.Json;

const string WebApiUrl = "https://localhost:7002/api/Weather";
const string RequestUrl = "?city={0}&country={1}";

var client = new HttpClient();
client.BaseAddress = new Uri(WebApiUrl);

bool isContinue;
string city, country, continueStr;

do
{
    isContinue = false;
    Console.WriteLine();
    Console.Write("Enter city name: ");
    city = Console.ReadLine();
    Console.Write("Enter country name: ");
    country = Console.ReadLine();
    Console.WriteLine();

    var response = await client.GetAsync(string.Format(RequestUrl, city, country));
    var json = await response.Content.ReadAsStringAsync();

    if (response.IsSuccessStatusCode)
    {
        var weatherDto = JsonSerializer.Deserialize<WeatherDTO>(json, ApplicationOptions.jsonSerializerOptions);
        Console.WriteLine(weatherDto);
    }
    else
        Console.WriteLine(json);

    Console.WriteLine("\n\nIf you want to continue enter 'y', else something another");
    continueStr = Console.ReadLine();
    if (continueStr == "y")
        isContinue = true;

} while (isContinue);
