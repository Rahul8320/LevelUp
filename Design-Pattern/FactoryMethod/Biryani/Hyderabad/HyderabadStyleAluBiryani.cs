namespace FactoryMethod.Biryani.Hyderabad;

public sealed class HyderabadStyleAluBiryani : IBiryani
{
    public void Box()
    {
        Console.WriteLine("Add Alu 1 pics, and Rices. Packaging done.");
    }

    public void Cook()
    {
        Console.WriteLine("Cooking for 30 minutes");
    }

    public string GetName()
    {
        return "Hyderabad Style Alu Biryani";
    }

    public void Prepare()
    {
        Console.WriteLine("Adding Rices, Adding salt, Adding oil, Adding Alu, Adding masala");
    }
}
