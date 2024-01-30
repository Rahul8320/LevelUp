using System.Linq.Expressions;
using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Infrastructure.DB;
using BookBoowingApp.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookBoowingApp.Infrastructure.RepositoryImplementations;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
{
    /// <summary>
    /// ApplicationDbContext
    /// </summary>
    private readonly ApplicationDbContext _context = context;

    /// <summary>
    /// Create new entity
    /// </summary>
    /// <param name="entity">Details of the entity</param>
    /// <exception cref="ApiException">Api Exception</exception>
    public async void Add(T entity)
    {
        try
        {
            if (entity == null)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Entity is empty!"));
            }

            await _context.Set<T>().AddAsync(entity);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Delete An Entity.
    /// </summary>
    /// <param name="entity">The Entity Details.</param>
    /// <exception cref="ApiException">Api Exception</exception>
    public void Delete(T entity)
    {
        try
        {
            if (entity == null)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Entity is empty!"));
            }

            _context.Set<T>().Remove(entity);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Update an existing entity.
    /// </summary>
    /// <param name="entity">Updated entity</param>
    /// <exception cref="ApiException">Api exception</exception>
    public void Update(T entity)
    {
        try
        {
            if (entity == null)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Entity is empty!"));
            }

            _context.Set<T>().Update(entity);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Find an entity details.
    /// </summary>
    /// <param name="predicate">The base function which bases find is performed.</param>
    /// <returns>Return an entity if found or return null.</returns>
    public async Task<T?> Find(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);

    /// <summary>
    /// Get an entity details by it's id
    /// </summary>
    /// <param name="id">The id of the entity.</param>
    /// <returns>Return entity details if found or return null.</returns>
    public async Task<T?> Get(Guid id) => await _context.Set<T>().FindAsync(id);

    /// <summary>
    /// Get list of all entities present in database.
    /// </summary>
    /// <returns>Return list of entity.</returns>
    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
}
