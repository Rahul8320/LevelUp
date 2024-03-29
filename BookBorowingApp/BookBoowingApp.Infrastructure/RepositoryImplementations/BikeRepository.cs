using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

/// <summary>
/// Represents the implementation for bike repository interface.
/// </summary>
/// <param name="context">Application Db Context</param>
public class BikeRepository(ApplicationDbContext context) : Repository<Bike>(context), IBikeRepository
{
    /// <summary>
    /// Application Db Context
    /// </summary>
    private readonly ApplicationDbContext _context = context;
}
