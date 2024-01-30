using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

/// <summary>
/// Represent the implementation of agreement repository.
/// </summary>
/// <param name="context"></param>
public class AgreementRepository(ApplicationDbContext context) : Repository<Agreement>(context), IAgreementRepository
{
    private readonly ApplicationDbContext _context = context;
}
