using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

/// <summary>
/// Represent the implementation for bike rating repository.
/// </summary>
/// <param name="context">Application db context.</param>
public class BikeRatingRepository(ApplicationDbContext context) : Repository<BikeRating>(context), IBikeRatingRepository
{
    private readonly ApplicationDbContext _context = context;
}
