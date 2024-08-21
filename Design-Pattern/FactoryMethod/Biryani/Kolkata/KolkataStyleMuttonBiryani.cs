namespace FactoryMethod.Biryani.Kolkata;

public sealed class KolkataStyleMuttonBiryani : IBiryani
{
    public void Box()
    {
        Console.WriteLine("Add Alu 1 pics, Mutton 1 pics, and Rices. Packaging done.");
    }

    public void Cook()
    {
        Console.WriteLine("Cooking for 65 minutes");
    }

    public string GetName()
    {
        return "Kolkata Style Mutton Biryani";
    }

    public void Prepare()
    {
        Console.WriteLine("Adding Rices, Adding salt, Adding oil, Adding Mutton, Adding Alu, Adding masala");
    }
}
