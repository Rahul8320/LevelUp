using AbstractFactory.Elements;
using AbstractFactory.Elements.Interfaces;
using AbstractFactory.Factories.Interfaces;

namespace AbstractFactory.Factories;

public sealed class LinuxFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new LinuxButton();
    }

    public ICheckBox CreateCheckBox()
    {
        return new LinuxCheckBox();
    }
}
