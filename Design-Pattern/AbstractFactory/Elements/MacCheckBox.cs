using AbstractFactory.Elements.Interfaces;

namespace AbstractFactory.Elements;

public sealed class MacCheckBox : ICheckBox
{
    public void Paint()
    {
        Console.WriteLine("Render a checkbox in MacOS style.");
    }
}
