namespace WeatherStationApp.Weather;

public sealed class WeatherStation : ISubject
{
    private readonly List<IObserver> observers = [];
    private WeatherDto weatherDto = new(0, 0, 0);

    public void SetWeather(WeatherDto weather)
    {
        weatherDto = weather;
        NotifyObservers();
    }

    public WeatherDto GetCurrentWeather()
    {
        return weatherDto;
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(weatherDto);
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
}
