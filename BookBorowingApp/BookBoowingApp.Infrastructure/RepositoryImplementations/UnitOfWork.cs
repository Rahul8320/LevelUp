using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

/// <summary>
/// Represents the implementation for unit of work interface.
/// </summary>
/// <param name="context">Application db context.</param>
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    /// <summary>
    /// Application Db Context
    /// </summary>
    private readonly ApplicationDbContext _context = context;

    #region repositories
    /// <summary>
    /// Represent bike repository interface
    /// </summary>
    private readonly IBikeRepository _bikeRepository = default!;

    /// <summary>
    /// Represent agreement repository interface. 
    /// </summary>
    private readonly IAgreementRepository _agreementRepository = default!;

    /// <summary>
    /// Represent bike rating repository interface.
    /// </summary>
    private readonly IBikeRatingRepository _bikeRatingRepository = default!;

    /// <summary>
    /// Get bike repository interface.
    /// </summary>
    public IBikeRepository BikeRepository
    {
        get
        {
            return _bikeRepository ?? new BikeRepository(_context);
        }
    }

    /// <summary>
    /// Get agreement repository interface.
    /// </summary>
    public IAgreementRepository AgreementRepository
    {
        get
        {
            return _agreementRepository ?? new AgreementRepository(_context);
        }
    }

    /// <summary>
    /// Get bike rating repository interface.
    /// </summary>
    public IBikeRatingRepository BikeRatingRepository
    {
        get
        {
            return _bikeRatingRepository ?? new BikeRatingRepository(_context);
        }
    }

    #endregion repositories

    /// <summary>
    /// Get the status of the operations.
    /// </summary>
    /// <returns>Returns true or false.</returns>
    public async Task<bool> Complete() => await _context.SaveChangesAsync() > 0;

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
