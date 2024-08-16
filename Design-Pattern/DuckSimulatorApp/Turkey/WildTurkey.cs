namespace DuckSimulatorApp.Turkey;

public sealed class WildTurkey : ITurkey
{
    public void Fly()
    {
        Console.WriteLine("Wild Turkey is Flying....");
    }

    public void Gobble()
    {
        Console.WriteLine("Wild Turkey is Gobbling...");
    }
}
