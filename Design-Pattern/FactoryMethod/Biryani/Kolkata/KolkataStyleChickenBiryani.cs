namespace FactoryMethod.Biryani.Kolkata;

public sealed class KolkataStyleChickenBiryani : IBiryani
{
    public void Box()
    {
        Console.WriteLine("Add Alu 1 pics, Chicken 1 pics, and Rices. Packaging done.");
    }

    public void Cook()
    {
        Console.WriteLine("Cooking for 45 minutes");
    }

    public string GetName()
    {
        return "Kolkata Style Chicken Biryani";
    }

    public void Prepare()
    {
        Console.WriteLine("Adding Rices, Adding salt, Adding oil, Adding Chicken, Adding Alu, Adding masala");
    }
}
