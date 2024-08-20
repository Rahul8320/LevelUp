using System.Runtime.InteropServices;
using AbstractFactory;
using AbstractFactory.Factories;
using AbstractFactory.Factories.Interfaces;

Console.WriteLine("Abstract Factory Design Pattern Implementation!");

IGUIFactory factory = GetFactoryBasedOnOS();

Application app = new(factory);

app.CreateUI();
app.Paint();

Console.WriteLine("Execution Completed");


static IGUIFactory GetFactoryBasedOnOS()
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        return new WinFactory();
    }

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        return new LinuxFactory();
    }

    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        return new MacFactory();
    }

    throw new Exception("Error! Unknown operating system.");
}