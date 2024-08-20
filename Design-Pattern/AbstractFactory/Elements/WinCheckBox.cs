using AbstractFactory.Elements.Interfaces;

namespace AbstractFactory.Elements;

public sealed class WinCheckBox : ICheckBox
{
    public void Paint()
    {
        Console.WriteLine("Render a checkbox in Windows style.");
    }
}
