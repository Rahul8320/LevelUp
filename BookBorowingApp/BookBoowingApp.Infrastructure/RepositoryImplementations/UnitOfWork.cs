using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    /// <summary>
    /// Application Db Context
    /// </summary>
    private readonly ApplicationDbContext _context = context;

    #region repositories
    private readonly IBikeRepository _bikeRepository = default!;
    public IBikeRepository BikeRepository
    {
        get
        {
            return _bikeRepository ?? new BikeRepository(_context);
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
