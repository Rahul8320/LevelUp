using Builder.Builders.Interfaces;
using Builder.Engines.Interfaces;
using Builder.Models;

namespace Builder.Builders;

public sealed class CarManualBuilder : IBuilder
{
    private Manual _manual;

    public CarManualBuilder()
    {
        _manual = new Manual();
    }

    public void Reset()
    {
        _manual = new Manual();
    }

    public void SetAdasAvailability(bool adasAvailability)
    {
        _manual.IsAdasAvailable = adasAvailability;
    }

    public void SetEngine(IEngine engine)
    {
        _manual.Engine = engine.Details();
    }

    public void SetGPS(string gps)
    {
        _manual.GPS = gps;
    }

    public void SetMilage(string milage)
    {
        _manual.Milage = milage;
    }

    public void SetModel(CarModel model)
    {
        _manual.Model = model;
    }

    public void SetRange(string range)
    {
        _manual.Range = range;
    }

    public void SetSeats(int seats)
    {
        _manual.Seats = seats;
    }

    public void SetTopSpeed(int topSpeed)
    {
        _manual.TopSpeed = topSpeed;
    }

    public void SetTripComputer(string tripComputer)
    {
        _manual.TripComputer = tripComputer;
    }

    public Manual GetProduct()
    {
        var product = _manual;
        Reset();
        return product;
    }
}
