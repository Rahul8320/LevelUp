namespace WeatherStationApp.Weather;

public sealed class Logger : IObserver
{
    private readonly ISubject subject;
    private WeatherDto weatherDto;

    public Logger(ISubject subject)
    {
        this.subject = subject;
        subject.RegisterObserver(this);
        weatherDto = this.subject.GetCurrentWeather();
    }

    public void Update(WeatherDto weatherDto)
    {
        this.weatherDto = weatherDto;
        Log();
    }

    public void Log()
    {
        Console.WriteLine($"[{DateTime.Now.ToLocalTime()}] - Temperature: {weatherDto.Temperature}, Wind Speed: {weatherDto.WindSpeed}, Pressure: {weatherDto.Pressure}");
    }
}
