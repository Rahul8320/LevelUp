namespace BookBoowingApp.Infrastructure.IRepositories;

/// <summary>
/// Represents the interface for unit of work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Get the interface for bike repository.
    /// </summary>
    IBikeRepository BikeRepository { get; }

    /// <summary>
    /// Get the interface for agreement repository.
    /// </summary>
    IAgreementRepository AgreementRepository { get; }

    /// <summary>
    /// Get the interface for bike rating repository.
    /// </summary>
    IBikeRatingRepository BikeRatingRepository { get; }

    /// <summary>
    /// Complete function
    /// </summary>
    /// <returns>Returns boolean result.</returns>
    Task<bool> Complete();
}
