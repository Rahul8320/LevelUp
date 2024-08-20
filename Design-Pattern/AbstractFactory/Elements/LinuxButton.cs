using AbstractFactory.Elements.Interfaces;

namespace AbstractFactory.Elements;

public sealed class LinuxButton : IButton
{
    public void Paint()
    {
        Console.WriteLine("Button Created Using Linux UI Style");
    }
}
