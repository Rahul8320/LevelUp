using BookBoowingApp.Domain.Enums;

namespace BookBoowingApp.Domain.Entities;

/// <summary>
/// Represent bike entity.
/// </summary>
public class Bike
{
    /// <summary>
    /// Get or set bike id.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets bike owner id.
    /// </summary>
    public Guid Owner { get; set; }
    /// <summary>
    /// Gets or set bike maker.
    /// </summary>
    public string Maker { get; set; } = default!;
    /// <summary>
    /// Gets or sets bike model.
    /// </summary>
    public string Model { get; set; } = default!;
    /// <summary>
    /// Gets or sets rental price per day.
    /// </summary>
    public int RentalPricePerDay { get; set; }
    /// <summary>
    /// Gets or sets late fees per day.
    /// </summary>
    public int LateFeesPerDay { get; set; }
    /// <summary>
    /// Gets or sets is available for rent.
    /// </summary>
    public bool IsAvailableForRent { get; set; }
    /// <summary>
    /// Gets or sets is request for return.
    /// </summary>
    public bool IsRequestForReturn { get; set; }
    /// <summary>
    /// Gets or sets current bike status.
    /// </summary>
    public BikeStatus CurrentBikeStatus { get; set; }
    /// <summary>
    /// Gets or sets cover image.
    /// </summary>
    public string CoverImage { get; set; } = default!;
    /// <summary>
    /// Gets or sets images.
    /// </summary>
    public List<string> Images { get; set; } = default!;
    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public string Description { get; set; } = default!;
    /// <summary>
    /// Gets or sets fuel capacity.
    /// </summary>
    public int FuelCapacity { get; set; }
    /// <summary>
    /// Gets or sets fuel economy.
    /// </summary>
    public int FuelEconomy { get; set; }
    /// <summary>
    /// Gets or sets created at.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; }
}
