namespace DuckSimulatorApp.Duck;

public sealed class MallardDuck : IDuck
{
    public void Fly()
    {
        Console.WriteLine("Mallard Duck is Flying...");
    }

    public void Quack()
    {
        Console.WriteLine("Mallard Duck is Quacking....");
    }
}
