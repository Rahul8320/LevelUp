namespace WeatherStationApp.Weather;

public interface IObserver
{
    void Update(WeatherDto weatherDto);
}
