using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

public class BikeRepository(ApplicationDbContext context) : Repository<Bike>(context), IBikeRepository
{
    /// <summary>
    /// Application Db Context
    /// </summary>
    private readonly ApplicationDbContext _context = context;
}
