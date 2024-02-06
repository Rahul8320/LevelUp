namespace BookBoowingApp.Domain.Enums;

/// <summary>
/// Represents bike status enum.
/// </summary>
public enum BikeStatus
{
    /// <summary>
    /// Represents available for rent status.
    /// </summary>
    AvailableForRent,
    /// <summary>
    /// Represents rented status.
    /// </summary>
    Rented,
    /// <summary>
    /// Represents request for return status.
    /// </summary>
    RequestForReturn,
    /// <summary>
    /// Represents hold for inspection status.
    /// </summary>
    HoldForInspection,
    /// <summary>
    /// Represents returned status.
    /// </summary>
    Returned
}
