using AbstractFactory.Elements.Interfaces;

namespace AbstractFactory.Elements;

public sealed class WinButton : IButton
{
    public void Paint()
    {
        Console.WriteLine("Render a button in Windows style.");
    }
}
