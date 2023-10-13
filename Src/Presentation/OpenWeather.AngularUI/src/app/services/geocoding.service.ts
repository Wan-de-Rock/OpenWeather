import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class GeocodingService {
    private api_url = "https://localhost:7020";

    constructor(private http: HttpClient) { }

    getCountriesNamesSorted(): Observable<string[]> {
        let url_weather = `${this.api_url}/api/Geocoding`;

        return this.http.get<string[]>(url_weather);
    }
}