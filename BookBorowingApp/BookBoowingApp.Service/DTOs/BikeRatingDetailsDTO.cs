using BookBoowingApp.Domain.Entities;

namespace BookBoowingApp.Service.DTOs;

public class BikeRatingDetailsDTO
{
    /// <summary>
    /// Gets or sets average rating.
    /// </summary>
    public int AverageRating { get; set; }

    /// <summary>
    /// Gets or sets number of rating.
    /// </summary>
    public double NumberOfRating { get; set; }

    /// <summary>
    /// Gets or sets bike ratings.
    /// </summary>
    public List<BikeRating> BikeRatings { get; set; } = default!;
}
