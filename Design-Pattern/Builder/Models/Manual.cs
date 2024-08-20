namespace Builder.Models;

public sealed class Manual
{
    public int Seats { get; set; }
    public CarModel Model { get; set; }
    public string Engine { get; set; } = string.Empty;
    public int TopSpeed { get; set; }
    public string? GPS { get; set; }
    public string? TripComputer { get; set; }
    public bool IsAdasAvailable { get; set; } = false;
    public string? Milage { get; set; }
    public string? Range { get; set; }

    public void Details()
    {
        Console.WriteLine("----------- Car Details ----------------");
        Console.WriteLine($"Car Manual - {Model} Car.");
        Console.WriteLine($"Available Seats: {Seats}, Engine: {Engine}, Top Speed: {TopSpeed}.");
        Features();
    }

    public void Features()
    {
        Console.WriteLine("-------------- Car Features Manual --------------");
        Console.WriteLine($"GPS : {GPS ?? "Not Available"}.");
        Console.WriteLine($"Trip Computer : {TripComputer ?? "Not Available"}.");

        var adasAvailable = IsAdasAvailable ? "Available" : "Not Available";
        Console.WriteLine($"Adas : {adasAvailable}.");

        if (Model == CarModel.EV)
        {
            Console.WriteLine($"Range: {Range ?? "Data Not Available"}");
        }
        else
        {
            Console.WriteLine($"Milage: {Milage ?? "Data Not Available"}");
        }
    }
}
