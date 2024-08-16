namespace WeatherStationApp.Weather;

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
    WeatherDto GetCurrentWeather();
}
