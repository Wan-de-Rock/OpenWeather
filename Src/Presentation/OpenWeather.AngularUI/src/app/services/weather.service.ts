import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Weather } from "../models/weather.model";
import { Observable } from "rxjs";
import { environment } from "environments/environment.prod";

@Injectable({
    providedIn: 'root'
})
export class WeatherService {

    constructor(private http: HttpClient) { }

    getWeather(city: string, country: string): Observable<Weather> {
        let url_weather = `${environment.OPENWEATHER_API_BASE_URL}/Weather?city=${city}&country=${country}`;

        return this.http.get<Weather>(url_weather);
    }
}