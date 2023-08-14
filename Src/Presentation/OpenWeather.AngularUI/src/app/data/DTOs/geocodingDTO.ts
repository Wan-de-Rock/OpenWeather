export class GeocodingDto {
  name: string
  local_names?: string[]
  lat: number
  lon: number
  country: string
  state?: string

  constructor(
    name = "",
    lat = 0,
    lon = 0,
    country = ""
  ) {
    this.name = name;
    this.lat = lat;
    this.lon = lon;
    this.country = country;
  }
}