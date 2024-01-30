namespace BookBoowingApp.Infrastructure.IRepositories;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Interface for bike repository.
    /// </summary>
    IBikeRepository BikeRepository { get; }

    /// <summary>
    /// Complete function
    /// </summary>
    /// <returns>Returns boolean result.</returns>
    Task<bool> Complete();
}
