import { Component, Input } from '@angular/core';
import { WeatherDto } from 'src/app/data/DTOs/weatherDTO';
import { Weather } from 'src/app/models/weather.model';
import { WeatherService } from 'src/app/services/weather.service';

@Component({
    selector: 'app-weather',
    templateUrl: './weather.component.html',
    styleUrls: ['./weather.component.css']
})
export class WeatherComponent {
    cityName: string = "";
    countryName: string = "";
    @Input()
    weather = new Weather();

    constructor(private weatherService: WeatherService) { }

    async onSubmit() {
        this.weather = await
        this.weatherService.getWeather(this.cityName, this.countryName);
    }
}