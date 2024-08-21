namespace FactoryMethod.Biryani.Kolkata;

public sealed class KolkataStyleEggBiryani : IBiryani
{
    public void Box()
    {
        Console.WriteLine("Add Alu 1 pics, Egg 1 pics, and Rices. Packaging done.");
    }

    public void Cook()
    {
        Console.WriteLine("Cooking for 30 minutes");
    }

    public string GetName()
    {
        return "Kolkata Style Egg Biryani";
    }

    public void Prepare()
    {
        Console.WriteLine("Adding Rices, Adding salt, Adding oil, Adding Egg, Adding Alu, Adding masala");
    }
}
