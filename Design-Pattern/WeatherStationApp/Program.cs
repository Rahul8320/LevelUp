using WeatherStationApp.Demo;
using WeatherStationApp.Weather;

Console.WriteLine("Welcome to the Weather Station App!");

SimpleSubject subject = new();
SimpleObserver observer = new(subject);

observer.Display();
subject.SetValue(26);
subject.SetValue(49);

Console.WriteLine("-------------------------------------------");

WeatherStation weatherStation = new();
UserInterface userInterface = new(weatherStation);
Logger logger = new(weatherStation);
Alert alert = new(weatherStation);

weatherStation.SetWeather(new WeatherDto(26, 12, 1.68));