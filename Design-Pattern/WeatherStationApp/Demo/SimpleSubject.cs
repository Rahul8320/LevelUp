namespace WeatherStationApp.Demo;

public class SimpleSubject : ISubject
{
    private readonly List<IObserver> _observers = [];
    private int _value = 0;

    public void SetValue(int value)
    {
        _value = value;
        NotifyObservers();
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_value);
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }
}
