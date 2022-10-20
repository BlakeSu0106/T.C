using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Telligent.Core.Domain.Entities;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Database;

namespace Telligent.Core.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly BaseDbContext _context;
    protected readonly DbSet<T> DbSet;

    /// <summary>
    /// implement a generic repository based on entity framework
    /// </summary>
    /// <param name="context">entity framework core context</param>
    public BaseRepository(BaseDbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }

    /// <summary>
    /// get entity by linq query
    /// <para>do not forgot set entity_status = true if you'd like to get activated item only</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// get entity list by linq
    /// <para>do not forgot set entity_status = true if you'd like to get activated items only</para>
    /// </summary>
    /// <param name="predicate">linq query</param>
    /// <returns>entities</returns>
    public async Task<IList<T>> GetListAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="ordered"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public async Task<IList<T>> GetOrderedTopNListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, dynamic>> ordered, int top)
    {
        return await DbSet.Where(predicate).OrderBy(ordered).Take(top).ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="ordered"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public async Task<IList<T>> GetOrderedDescTopNListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, dynamic>> ordered, int top)
    {
        return await DbSet.Where(predicate).OrderByDescending(ordered).Take(top).ToListAsync();
    }

    /// <summary>
    /// insert entity into database
    /// </summary>
    /// <param name="entity">entity</param>
    public async Task CreateAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    /// <summary>
    /// insert multiple entities into database with transaction
    /// </summary>
    /// <param name="entities">entities</param>
    public async Task CreateAsync(IList<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    /// <summary>
    /// update entity
    /// </summary>
    /// <param name="entity">entity</param>
    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    /// <summary>
    /// update entities
    /// </summary>
    /// <param name="entities">entities</param>
    public void Update(IList<T> entities)
    {
        DbSet.UpdateRange(entities);
    }

    /// <summary>
    /// delete entities by linq query
    /// </summary>
    /// <param name="predicate"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var entities = await GetListAsync(predicate);

        if (entities == null || entities.Count == 0)
            return;

        DbSet.RemoveRange(entities);
    }

    /// <summary>
    /// delete entities
    /// </summary>
    /// <param name="entities"></param>
    public void Delete(IList<T> entities)
    {
        DbSet.RemoveRange(entities);
    }

    /// <summary>
    /// save changes
    /// </summary>
    /// <returns>change count</returns>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}