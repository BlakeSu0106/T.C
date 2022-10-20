using Microsoft.EntityFrameworkCore;
using Telligent.Core.Domain.Entities;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Database;
using Telligent.Core.Infrastructure.Extensions;

namespace Telligent.Core.Infrastructure.Repositories;

public class Repository<T> : BaseRepository<T>, IRepository<T> where T : Entity
{
    /// <summary>
    /// implement a generic repository based on entity framework
    /// </summary>
    /// <param name="context">entity framework core context</param>
    public Repository(BaseDbContext context) : base(context)
    {
    }

    /// <summary>
    /// get entity by id (entity status equals to activated)
    /// </summary>
    /// <param name="id">guid id primary key</param>
    /// <returns>entity</returns>
    public async Task<T> GetAsync(Guid id)
    {
        return await DbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id) && entity.EntityStatus);
    }

    /// <summary>
    /// insert entity into database
    /// <para>auto set entity status to true and creation time to now</para>
    /// </summary>
    /// <param name="entity">entity</param>
    public new async Task CreateAsync(T entity)
    {
        entity.EntityStatus = true;
        entity.CreationTime = DateTime.UtcNow.ToUtc8DateTime();

        await DbSet.AddAsync(entity);
    }

    /// <summary>
    /// insert multiple entities into database with transaction
    /// <para>auto set entity status to true and creation time to now</para>
    /// </summary>
    /// <param name="entities">entities</param>
    public new async Task CreateAsync(IList<T> entities)
    {
        var now = DateTime.UtcNow.ToUtc8DateTime();

        foreach (var entity in entities)
        {
            entity.EntityStatus = true;
            entity.CreationTime = now;
        }

        await DbSet.AddRangeAsync(entities);
    }

    /// <summary>
    /// update entity
    /// <para>auto set modification time to now</para>
    /// </summary>
    /// <param name="entity">entity</param>
    public new void Update(T entity)
    {
        entity.ModificationTime = DateTime.UtcNow.ToUtc8DateTime();

        DbSet.Update(entity);
    }

    /// <summary>
    /// update entities
    /// <para>auto set modification time to now</para>
    /// </summary>
    /// <param name="entities">entities</param>
    public new void Update(IList<T> entities)
    {
        var now = DateTime.UtcNow.ToUtc8DateTime();

        foreach (var entity in entities) entity.ModificationTime = now;

        DbSet.UpdateRange(entities);
    }

    /// <summary>
    /// delete entity
    /// <para>auto set entity status to false and deletion time to now</para>
    /// </summary>
    /// <param name="id">entity's id</param>
    /// <param name="deleterId">deleter id</param>
    public async Task DeleteAsync(Guid id, Guid deleterId)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity == null)
            return;

        entity.EntityStatus = false;
        entity.DeleterId = deleterId;
        entity.DeletionTime = DateTime.UtcNow.ToUtc8DateTime();

        DbSet.Update(entity);
    }

    /// <summary>
    /// hard delete entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task HardDeleteAsync(Guid id)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity == null)
            return;

        DbSet.Remove(entity);
    }
}