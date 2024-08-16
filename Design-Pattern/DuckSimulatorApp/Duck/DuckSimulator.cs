namespace DuckSimulatorApp.Duck;

public class DuckSimulator
{
    public static void Run()
    {
        // Test a duck
        IDuck mallardDuck = new MallardDuck();

        TestDuck(mallardDuck);
    }

    public static void TestDuck(IDuck duck)
    {
        duck.Quack();
        duck.Fly();
    }
}
