using AbstractFactory.Elements.Interfaces;

namespace AbstractFactory.Elements;

public sealed class MacButton : IButton
{
    public void Paint()
    {
        Console.WriteLine("Render a button in macOS style.");
    }
}
