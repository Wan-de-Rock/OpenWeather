export class Weather {
  temperature: number;
  humidity: number;
  windSpeed: number;
  cloudiness: number;
  description: string;

  constructor(
    temperature = 0,
    humidity = 0.0,
    windSpeed = 0,
    cloudiness = 0.0,
    description = "no description"
  ) {
    this.temperature = temperature;
    this.humidity = humidity;
    this.windSpeed = windSpeed;
    this.cloudiness = cloudiness;
    this.description = description;
  }
}