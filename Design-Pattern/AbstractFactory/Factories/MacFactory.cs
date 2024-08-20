using AbstractFactory.Elements;
using AbstractFactory.Elements.Interfaces;
using AbstractFactory.Factories.Interfaces;

namespace AbstractFactory.Factories;

public sealed class MacFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new MacButton();
    }

    public ICheckBox CreateCheckBox()
    {
        return new MacCheckBox();
    }
}
