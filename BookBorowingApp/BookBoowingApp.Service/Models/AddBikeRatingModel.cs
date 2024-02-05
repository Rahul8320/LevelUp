using System.ComponentModel.DataAnnotations;

namespace BookBoowingApp.Service.Models;

public class AddBikeRatingModel
{
    /// <summary>
    /// Gets or sets bike id.
    /// </summary>
    [Required(ErrorMessage = "BikeId cannot be empty!")]
    public Guid BikeId { get; set; }
    /// <summary>
    /// Gets or sets rating.
    /// </summary>
    [Required(ErrorMessage = "Rating cannot be empty!")]
    [Range(1, 5, ErrorMessage = "Rating must be in range [1 ~ 5]")]
    public int Rating { get; set; }
    /// <summary>
    /// Gets or sets review.
    /// </summary>
    [Required(ErrorMessage = "Review cannot be empty!")]
    [MinLength(10, ErrorMessage = "Review must be 10 character long!")]
    [MaxLength(500, ErrorMessage = "Review cannot be more than 500 character long!")]
    public string Review { get; set; } = default!;
}
