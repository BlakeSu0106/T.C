using System.Linq.Expressions;
using Telligent.Core.Domain.Entities;

namespace Telligent.Core.Domain.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    /// <summary>
    /// get entity by linq query
    /// <para>do not forgot set entity_status = true if you'd like to get activated item only</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// get entity list by linq
    /// <para>do not forgot set entity_status = true if you'd like to get activated items only</para>
    /// </summary>
    /// <param name="predicate">linq query</param>
    /// <returns>entities</returns>
    Task<IList<T>> GetListAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// get top N ordered list by linq
    /// </summary>
    /// <param name="predicate"></param>
    /// /// <param name="ordered"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    Task<IList<T>> GetOrderedTopNListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, dynamic>> ordered, int top);

    /// <summary>
    /// get top N ordered desc list by linq
    /// </summary>
    /// <param name="predicate"></param>
    /// /// <param name="ordered"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    Task<IList<T>> GetOrderedDescTopNListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, dynamic>> ordered, int top);

    /// <summary>
    /// insert entity into database
    /// <para>auto set entity status to true and creation time to now</para>
    /// </summary>
    /// <param name="entity">entity</param>
    Task CreateAsync(T entity);

    /// <summary>
    /// insert multiple entities into database with transaction
    /// <para>auto set entity status to true and creation time to now</para>
    /// </summary>
    /// <param name="entities">entities</param>
    Task CreateAsync(IList<T> entities);

    /// <summary>
    /// update entity
    /// <para>auto set modification time to now</para>
    /// </summary>
    /// <param name="entity">entity</param>
    void Update(T entity);

    /// <summary>
    /// update entities
    /// <para>auto set modification time to now</para>
    /// </summary>
    /// <param name="entities">entities</param>
    void Update(IList<T> entities);

    /// <summary>
    /// delete entities by linq query
    /// </summary>
    /// <param name="predicate"></param>
    Task DeleteAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// delete multiple entities
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    void Delete(IList<T> entities);

    /// <summary>
    /// save changes
    /// </summary>
    /// <returns>change count</returns>
    Task<int> SaveChangesAsync();
}