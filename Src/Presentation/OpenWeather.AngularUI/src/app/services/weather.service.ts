import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { WeatherDto } from "../data/DTOs/weatherDTO";
import { Observable, delay } from "rxjs";
import { GeocodingDto } from "../data/DTOs/geocodingDTO";
import { Weather } from "../models/weather.model";

@Injectable({
    providedIn: 'root'
})
export class WeatherService {
    private api_key = "e4a87cdd99b53079747a485abb15888b";
    constructor(private http: HttpClient) { }

    async getGeocoding(city: string, country: string): Promise<GeocodingDto> {
        let url_geocoding = `http://api.openweathermap.org/geo/1.0/direct?q=${city},${country}&limit=1&appid=${this.api_key}`;
        let geocoding: GeocodingDto = new GeocodingDto();

        return new Promise((resolve) => {
            this.http.get<GeocodingDto[]>(url_geocoding).subscribe({
                next: (response: GeocodingDto[]) => {
                    /*
                    geocoding = new GeocodingDto(
                        response[0].name,
                        response[0].lat,
                        response[0].lon,
                        response[0].country
                    );
                    */
                    geocoding.lat = response[0].lat;
                    geocoding.lon = response[0].lon;
                    geocoding.name = response[0].name;
                    geocoding.country = response[0].country;
                    geocoding.local_names = response[0].local_names;

                    resolve(geocoding);
                }
            });
        });
    }

    async getWeather(city: string, country: string): Promise<Weather> {
        let weather: Weather = new Weather();
        let geocoding: GeocodingDto = await this.getGeocoding(city, country);

        let url_weather = `https://api.openweathermap.org/data/2.5/weather?lat=${geocoding.lat}&lon=${geocoding.lon}&appid=${this.api_key}&units=metric`;

        return new Promise((resolve) => {
            this.http.get<WeatherDto>(url_weather).subscribe({
                next: (response: WeatherDto) => {
                    weather.temperature = response.main.temp;
                    weather.cloudiness = response.clouds.all;
                    weather.humidity = response.main.humidity;
                    weather.windSpeed = response.wind.speed;
                    weather.description = response.weather[0].description;

                    resolve(weather);
                }
            });

        });
    }
}