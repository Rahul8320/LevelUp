using System.ComponentModel.DataAnnotations;

namespace BookBoowingApp.Service.Models;

public class AddBikeModel
{
    /// <summary>
    /// Gets or sets bike maker.
    /// </summary>
    [Required(ErrorMessage = "Maker cannot be empty!")]
    [MinLength(3, ErrorMessage = "Maker must be 3 character long!")]
    [MaxLength(50, ErrorMessage = "Maker cannot be more than 50 character!")]
    public string Maker { get; set; } = default!;

    /// <summary>
    /// Gets or sets bike model
    /// </summary>
    [Required(ErrorMessage = "Model cannot be empty!")]
    [MinLength(3, ErrorMessage = "Model must be 3 character long!")]
    [MaxLength(50, ErrorMessage = "Model cannot be more than 50 character!")]
    public string Model { get; set; } = default!;

    /// <summary>
    /// Gets or sets rental price per day.
    /// </summary>
    [Required(ErrorMessage = "RentalPricePerDay cannot be empty!")]
    [Range(100, 10000)]
    public int RentalPricePerDay { get; set; }

    /// <summary>
    /// Gets or sets late fees per day
    /// </summary>
    [Required(ErrorMessage = "LateFeesPerDay cannot be empty!")]
    [Range(100, 10000)]
    public int LateFeesPerDay { get; set; }

    /// <summary>
    /// Gets or sets cover image.
    /// </summary>
    [Required(ErrorMessage = "CoverImage cannot be empty!")]
    [Url]
    public string CoverImage { get; set; } = default!;

    /// <summary>
    /// Gets or sets images.
    /// </summary>
    [Required(ErrorMessage = "Images cannot be empty!")]
    [MinLength(1, ErrorMessage = "Images must contains at least one image url!")]
    public List<string> Images { get; set; } = default!;

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    [Required(ErrorMessage = "Description cannot be empty!")]
    [MinLength(30, ErrorMessage = "Description must be 30 character long!")]
    [MaxLength(500, ErrorMessage = "Description cannot be more than 500 character!")]
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets or sets fuel capacity.
    /// </summary>
    [Required(ErrorMessage = "FuelCapacity cannot be empty!")]
    [Range(5, 25)]
    public int FuelCapacity { get; set; }

    /// <summary>
    /// Gets or sets fuel economy.
    /// </summary>
    [Required(ErrorMessage = "FuelEconomy cannot be empty!")]
    [Range(5, 75)]
    public int FuelEconomy { get; set; }
}
