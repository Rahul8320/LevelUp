namespace DuckSimulatorApp.Turkey;

public class WildTurkey : ITurkey
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
