namespace FactoryMethod.Biryani.Hyderabad;

public sealed class HyderabadStyleMuttonBiryani : IBiryani
{
    public void Box()
    {
        Console.WriteLine("Add Mutton 1 pics, and Rices. Packaging done.");
    }

    public void Cook()
    {
        Console.WriteLine("Cooking for 65 minutes");
    }

    public string GetName()
    {
        return "Hyderabad Style Mutton Biryani";
    }

    public void Prepare()
    {
        Console.WriteLine("Adding Rices, Adding salt, Adding oil, Adding Mutton, Adding masala");
    }
}
