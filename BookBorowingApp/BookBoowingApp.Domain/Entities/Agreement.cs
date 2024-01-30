namespace BookBoowingApp.Domain.Entities;

public class Agreement
{
    public Guid Id { get; set; }
    public Guid BikeId { get; set; }
    public Guid UserId { get; set; }
    public Guid BikeOwnerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Duration { get; set; }
    public int TotalCost { get; set; }
    public bool IsAcceptedByUser { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
}
