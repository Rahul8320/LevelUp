using System.Linq.Expressions;

namespace BookBoowingApp.Infrastructure.IRepositories;

public interface IRepository<T> where T : class
{
    /// <summary>
    /// Get an entity details by it's id.
    /// </summary>
    /// <param name="id">The entity id.</param>
    /// <returns>Returns entity details or return null.</returns>
    Task<T?> Get(Guid id);

    /// <summary>
    /// Find entities based on predicate.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns>Returns entity if found or return null.</returns>
    Task<T?> Find(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get all entity details.
    /// </summary>
    /// <returns>Returns list of entities.</returns>
    Task<IEnumerable<T>> GetAll();

    /// <summary>
    /// Create new entity.
    /// </summary>
    /// <param name="entity">Entity details.</param>
    void Add(T entity);

    /// <summary>
    /// Update an existing entity
    /// </summary>
    /// <param name="entity">The updated entity details.</param>
    void Update(T entity);

    /// <summary>
    /// Delete an existing entity.
    /// </summary>
    /// <param name="entity">The existing entity details.</param>
    void Delete(T entity);
}
