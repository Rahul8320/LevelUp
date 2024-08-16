namespace WeatherStationApp.Weather;

public sealed class UserInterface : IObserver
{
    private readonly ISubject subject;
    private WeatherDto weatherDto;

    public UserInterface(ISubject subject)
    {
        this.subject = subject;
        subject.RegisterObserver(this);
        weatherDto = this.subject.GetCurrentWeather();
    }

    public void Update(WeatherDto weatherDto)
    {
        this.weatherDto = weatherDto;
        DisplayWeather();
    }

    public void DisplayWeather()
    {
        Console.WriteLine("------- User Interface -------");
        Console.WriteLine("Current Weather Report: ");
        Console.WriteLine($"Temperature: {weatherDto.Temperature}");
        Console.WriteLine($"Wind Speed: {weatherDto.WindSpeed}");
        Console.WriteLine($"Pressure: {weatherDto.Pressure}");
        Console.WriteLine("------------------------------");
    }
}
