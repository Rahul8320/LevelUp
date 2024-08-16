namespace WeatherStationApp.Demo;

public class SimpleObserver : IObserver
{
    private int _value;
    private readonly ISubject _subject;

    public SimpleObserver(ISubject subject)
    {
        _subject = subject;
        _subject.RegisterObserver(this);
    }
    public void Update(int value)
    {
        _value = value;
        Display();
    }

    public void Display()
    {
        Console.WriteLine($"Value is: {_value}");
    }
}
