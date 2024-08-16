using DuckSimulatorApp.Drone;
using DuckSimulatorApp.Duck;
using DuckSimulatorApp.Turkey;

namespace DuckSimulatorApp;

public class DuckSimulator
{
    public static void Run()
    {
        // Test a Duck
        IDuck mallardDuck = new MallardDuck();
        TestDuck(mallardDuck);

        // Test a Turkey
        ITurkey wildTurkey = new WildTurkey();
        IDuck turkeyAdapter = new TurkeyAdapter(wildTurkey);
        TestDuck(turkeyAdapter);

        // Test a Drone
        IDrone superDrone = new SuperDrone();
        IDuck droneAdapter = new DroneAdapter(superDrone);
        TestDuck(droneAdapter);
    }

    public static void TestDuck(IDuck duck)
    {
        duck.Quack();
        duck.Fly();
        Console.WriteLine();
    }
}
