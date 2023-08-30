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

    LOCALITY_NAME_VALIDATION_REGEX: string = "[A-Z]\\w*(\\W*[A-Z]\\w*)*";
    //LOCALITY_NAME_VALIDATION_REGEX: RegExp = new RegExp("(?:\\W*[A-Z]\\w*)+");
    //LOCALITY_NAME_VALIDATION_REGEX: RegExp = new RegExp('^(?=[^A-Z]*[A-Z])(?=[^a-z]*[a-z])(?=\\D*\\d)[A-Za-z\\d!$%@#£€*?&]{8,}$');

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