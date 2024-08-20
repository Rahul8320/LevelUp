using Builder.Engines.Interfaces;
using Builder.Models;

namespace Builder.Builders.Interfaces;

public interface IBuilder
{
    void Reset();
    void SetSeats(int seats);
    void SetModel(CarModel model);
    void SetEngine(IEngine engine);
    void SetTopSpeed(int topSpeed);
    void SetTripComputer(string tripComputer);
    void SetGPS(string gps);
    void SetAdasAvailability(bool adasAvailability);
    void SetMilage(string milage);
    void SetRange(string range);
}
