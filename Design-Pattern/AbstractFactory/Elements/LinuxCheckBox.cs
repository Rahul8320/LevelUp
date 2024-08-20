using AbstractFactory.Elements.Interfaces;

namespace AbstractFactory.Elements;

public sealed class LinuxCheckBox : ICheckBox
{
    public void Paint()
    {
        Console.WriteLine("CheckBox Created Using Linux UI Style");
    }
}
