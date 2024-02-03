namespace BookBoowingApp.Domain.Entities;

/// <summary>
/// Represents bike rating entity.
/// </summary>
public class BikeRating
{
    /// <summary>
    /// Gets or sets bike rating id.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets bike id.
    /// </summary>
    public Guid BikeId { get; set; }
    /// <summary>
    /// Gets or sets user id.
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Gets or sets rating.
    /// </summary>
    public int Rating { get; set; }
    /// <summary>
    /// Gets or sets review.
    /// </summary>
    public string Review { get; set; } = default!;
    /// <summary>
    /// Gets or sets last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; }
}
