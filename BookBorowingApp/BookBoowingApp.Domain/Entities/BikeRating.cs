namespace BookBoowingApp.Domain.Entities;

public class BikeRating
{
    public Guid Id { get; set; }
    public Guid BikeId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string Review { get; set; } = default!;
    public DateTime LastUpdated { get; set; }
}
