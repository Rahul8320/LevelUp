using Builder.Builders.Interfaces;
using Builder.Engines.Interfaces;
using Builder.Models;

namespace Builder.Builders;

public sealed class CarBuilder : IBuilder
{
    private Car _car;

    public CarBuilder()
    {
        _car = new Car();
    }

    public void Reset()
    {
        _car = new Car();
    }

    public void SetAdasAvailability(bool adasAvailability)
    {
        _car.IsAdasAvailable = adasAvailability;
    }

    public void SetEngine(IEngine engine)
    {
        _car.Engine = engine.Details();
    }

    public void SetGPS(string gps)
    {
        _car.GPS = gps;
    }

    public void SetMilage(string milage)
    {
        _car.Milage = milage;
    }

    public void SetModel(CarModel model)
    {
        _car.Model = model;
    }

    public void SetRange(string range)
    {
        _car.Range = range;
    }

    public void SetSeats(int seats)
    {
        _car.Seats = seats;
    }

    public void SetTopSpeed(int topSpeed)
    {
        _car.TopSpeed = topSpeed;
    }

    public void SetTripComputer(string tripComputer)
    {
        _car.TripComputer = tripComputer;
    }

    public Car GetProduct()
    {
        var product = _car;
        Reset();
        return product;
    }
}
