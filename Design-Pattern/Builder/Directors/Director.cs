using Builder.Builders.Interfaces;
using Builder.Engines;
using Builder.Models;

namespace Builder.Directors;

public sealed class Director
{
    public static void ConstructSportsCar(IBuilder builder)
    {
        builder.Reset();
        builder.SetSeats(2);
        builder.SetModel(CarModel.Sports);
        builder.SetEngine(new SportsEngine());
        builder.SetGPS("NAVIC GPS System");
        builder.SetTopSpeed(355);
    }

    public static void ConstructEVCar(IBuilder builder)
    {
        builder.Reset();
        builder.SetSeats(4);
        builder.SetModel(CarModel.EV);
        builder.SetEngine(new EVEngine());
        builder.SetTripComputer("Digital Trip Meter");
        builder.SetGPS("NAVIC GPS System");
        builder.SetTopSpeed(140);
        builder.SetRange("250 km in single charge");
    }

    public static void ConstructSUVCar(IBuilder builder)
    {
        builder.Reset();
        builder.SetSeats(5);
        builder.SetModel(CarModel.SUV);
        builder.SetEngine(new SUVEngine());
        builder.SetTripComputer("Digital Trip Meter");
        builder.SetAdasAvailability(true);
        builder.SetGPS("NAVIC GPS System");
        builder.SetTopSpeed(180);
        builder.SetMilage("25 kmpl");
    }
}
