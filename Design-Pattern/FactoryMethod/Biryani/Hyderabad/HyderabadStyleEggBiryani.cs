namespace FactoryMethod.Biryani.Hyderabad;

public sealed class HyderabadStyleEggBiryani : IBiryani
{
    public void Box()
    {
        Console.WriteLine("Add Egg 1 pics, and Rices. Packaging done.");
    }

    public void Cook()
    {
        Console.WriteLine("Cooking for 30 minutes");
    }

    public string GetName()
    {
        return "Hyderabad Style Egg Biryani";
    }

    public void Prepare()
    {
        Console.WriteLine("Adding Rices, Adding salt, Adding oil, Adding Egg, Adding masala");
    }
}
