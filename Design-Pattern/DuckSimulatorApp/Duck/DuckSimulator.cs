using DuckSimulatorApp.Turkey;

namespace DuckSimulatorApp.Duck;

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
    }

    public static void TestDuck(IDuck duck)
    {
        duck.Quack();
        duck.Fly();
        Console.WriteLine();
    }
}
