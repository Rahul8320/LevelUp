namespace FactoryMethod.Biryani.Hyderabad;

public sealed class HyderabadStyleChickenBiryani : IBiryani
{
    public void Box()
    {
        Console.WriteLine("Add Chicken 1 pics, and Rices. Packaging done.");
    }

    public void Cook()
    {
        Console.WriteLine("Cooking for 45 minutes");
    }

    public string GetName()
    {
        return "Hyderabad Style Chicken Biryani";
    }

    public void Prepare()
    {
        Console.WriteLine("Adding Rices, Adding salt, Adding oil, Adding Chicken, Adding masala");
    }
}
