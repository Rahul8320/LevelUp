namespace BookBoowingApp.Domain.Entities;

public class Bike
{
    public Guid Id { get; set; }
    public Guid Owner { get; set; }
    public string Maker { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int RentalPricePerDay { get; set; }
    public int LateFeesPerDay { get; set; }
    public bool IsAvailableForRent { get; set; }
    public bool IsRequestForReturn { get; set; }
    public BikeStatus CurrentBikeStatus { get; set; }
    public string CoverImage { get; set; } = default!;
    public List<string> Images { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int FuelCapacity { get; set; }
    public int FuelEconomy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
}
