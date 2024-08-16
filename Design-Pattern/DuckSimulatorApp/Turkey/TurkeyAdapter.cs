using DuckSimulatorApp.Duck;

namespace DuckSimulatorApp.Turkey;

public sealed class TurkeyAdapter(ITurkey turkey) : IDuck
{
    public void Fly()
    {
        for (int i = 0; i < 5; i++)
        {
            turkey.Fly();
        }
    }

    public void Quack()
    {
        turkey.Gobble();
    }
}
