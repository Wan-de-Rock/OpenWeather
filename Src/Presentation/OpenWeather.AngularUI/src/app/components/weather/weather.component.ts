import { Component, Input, OnInit } from '@angular/core';
import { Weather } from 'src/app/models/weather.model';
import { WeatherService } from 'src/app/services/weather.service';

@Component({
    selector: 'app-weather',
    templateUrl: './weather.component.html',
    styleUrls: ['./weather.component.css']
})
export class WeatherComponent implements OnInit {
    DEFAULT_CITY_NAME: string = "Paris";
    DEFAULT_COUNTRY_NAME: string = "France";

    cityName: string = "";
    countryName: string = "";
    @Input()
    weather = new Weather();

    constructor(private weatherService: WeatherService) { }

    ngOnInit() {
        this.getWeather(this.DEFAULT_CITY_NAME, this.DEFAULT_COUNTRY_NAME);
    }

    onSubmit() {
        this.getWeather(this.cityName, this.countryName);
    }

    private getWeather(city: string, country: string){
        //this.weather = await this.weatherService.getWeather(city, country);
        this.weatherService.getWeatherFromCustomApi(city, country)
        .subscribe((response: Weather) => (this.weather = response));
    }
}