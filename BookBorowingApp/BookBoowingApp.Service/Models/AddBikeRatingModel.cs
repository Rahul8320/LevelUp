namespace BookBoowingApp.Service.Models;

public class AddBikeRatingModel
{
    /// <summary>
    /// Gets or sets bike id.
    /// </summary>
    public Guid BikeId { get; set; }
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
}
