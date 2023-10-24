import { Component, Input, OnInit } from '@angular/core';
import { Weather } from 'src/app/models/weather.model';
import { GeocodingService } from 'src/app/services/geocoding.service';
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

    cityName: string = "";
    countryName: string = "";
    countriesNames: string[] = [];

    @Input()
    weather = new Weather();

    constructor(private weatherService: WeatherService,
                private geocodingService: GeocodingService) { }

    ngOnInit() {
        this.getCountriesNames();
        this.getWeather(this.DEFAULT_CITY_NAME, this.DEFAULT_COUNTRY_NAME);
    }

    onSubmit() {
        this.getWeather(this.cityName, this.countryName);
    }

    changeCountry(event: any){
        this.countryName = event.target.value;
    }

    private getWeather(city: string, country: string){
        this.weatherService.getWeather(city, country)
        .subscribe((response: Weather) => (this.weather = response));
    }

    private getCountriesNames(){
        this.geocodingService.getCountriesNamesSorted()
        .subscribe((response: string[]) => (this.countriesNames = response));
    }
}