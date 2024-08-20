using AbstractFactory.Elements.Interfaces;
using AbstractFactory.Factories.Interfaces;

namespace AbstractFactory;

public sealed class Application(IGUIFactory factory)
{
    private IButton? button;
    private ICheckBox? checkBox;

    public void CreateUI()
    {
        button = factory.CreateButton();
        checkBox = factory.CreateCheckBox();
    }

    public void Paint()
    {
        if (button is null || checkBox is null)
        {
            Console.WriteLine("Oops! Still under development.");
            return;
        }

        button.Paint();
        checkBox.Paint();
    }
}
