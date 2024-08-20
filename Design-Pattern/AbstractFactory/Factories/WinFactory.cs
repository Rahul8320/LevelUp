using AbstractFactory.Elements;
using AbstractFactory.Elements.Interfaces;
using AbstractFactory.Factories.Interfaces;

namespace AbstractFactory.Factories;

public sealed class WinFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new WinButton();
    }

    public ICheckBox CreateCheckBox()
    {
        return new WinCheckBox();
    }
}
