import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "environments/environment.prod";

@Injectable({
    providedIn: 'root'
})
export class GeocodingService {

    constructor(private http: HttpClient) { }

    getCountriesNamesSorted(): Observable<string[]> {
        return this.http.get<string[]>(`${environment.OPENWEATHER_API_BASE_URL}/Geocoding`);
    }
}