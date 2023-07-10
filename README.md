# Open Weather Application

This is a small project based on the [Open Weather API](https://openweathermap.org/)

### The application work principle
- at the input we get the names of the city and country
- send the input data to the [Open Weather Geocoding API](https://openweathermap.org/api/geocoding-api) and in response we get the coordinates of the city
- we send the received coordinates to the [Open Weather Data API](https://openweathermap.org/current) and in response we get the current weather in this city
- we send the weather information to the output

### Application development plans
- Add Angular user interface
- Add Docker container
- (Possibly) Add User Authentication