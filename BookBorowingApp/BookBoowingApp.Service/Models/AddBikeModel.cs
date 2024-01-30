using System.ComponentModel.DataAnnotations;

namespace BookBoowingApp.Service.Models;

public class AddBikeModel
{
    [Required(ErrorMessage = "Maker cannot be empty!")]
    [MinLength(3, ErrorMessage = "Maker must be 3 character long!")]
    [MaxLength(50, ErrorMessage = "Maker cannot be more than 50 character!")]
    public string Maker { get; set; } = default!;

    [Required(ErrorMessage = "Model cannot be empty!")]
    [MinLength(3, ErrorMessage = "Model must be 3 character long!")]
    [MaxLength(50, ErrorMessage = "Model cannot be more than 50 character!")]
    public string Model { get; set; } = default!;

    [Required(ErrorMessage = "RentalPricePerDay cannot be empty!")]
    [Range(100, 10000)]
    public int RentalPricePerDay { get; set; }

    [Required(ErrorMessage = "LateFeesPerDay cannot be empty!")]
    [Range(100, 10000)]
    public int LateFeesPerDay { get; set; }

    [Required(ErrorMessage = "CoverImage cannot be empty!")]
    [Url]
    public string CoverImage { get; set; } = default!;

    [Required(ErrorMessage = "Images cannot be empty!")]
    [MinLength(1, ErrorMessage = "Images must contains at least one image url!")]
    public List<string> Images { get; set; } = default!;

    [Required(ErrorMessage = "Description cannot be empty!")]
    [MinLength(30, ErrorMessage = "Description must be 30 character long!")]
    [MaxLength(500, ErrorMessage = "Description cannot be more than 500 character!")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "FuelCapacity cannot be empty!")]
    [Range(5, 25)]
    public int FuelCapacity { get; set; }

    [Required(ErrorMessage = "FuelEconomy cannot be empty!")]
    [Range(5, 75)]
    public int FuelEconomy { get; set; }
}
