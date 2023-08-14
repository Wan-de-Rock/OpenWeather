import { Component } from '@angular/core';
import { Weather } from './models/weather.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'OpenWeather.AngularUI';

  //weather!: Weather;
}
