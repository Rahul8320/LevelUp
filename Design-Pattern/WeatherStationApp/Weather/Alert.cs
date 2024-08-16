namespace WeatherStationApp.Weather;

public sealed class Alert : IObserver
{
    private WeatherDto weatherDto;

    public Alert(ISubject subject)
    {
        subject.RegisterObserver(this);
        weatherDto = subject.GetCurrentWeather();
    }

    public void Update(WeatherDto weatherDto)
    {
        this.weatherDto = weatherDto;
        ShowAlert();
    }

    public void ShowAlert()
    {
        Console.WriteLine("************** ALERT ***************** ");
        Console.WriteLine($"Temperature: {weatherDto.Temperature}, Wind Speed: {weatherDto.WindSpeed}, Pressure: {weatherDto.Pressure}");
        Console.WriteLine("* DON'T GO OUTSIDE *");
        Console.WriteLine("************************************** ");
    }
}
