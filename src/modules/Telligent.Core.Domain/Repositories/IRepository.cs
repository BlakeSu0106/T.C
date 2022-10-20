using Telligent.Core.Domain.Entities;

namespace Telligent.Core.Domain.Repositories;

public interface IRepository<T> : IBaseRepository<T> where T : Entity
{
    /// <summary>
    /// get entity by id (entity status equals to activated)
    /// </summary>
    /// <param name="id">guid id primary key</param>
    /// <returns>entity</returns>
    Task<T> GetAsync(Guid id);

    /// <summary>
    /// delete entity
    /// <para>auto set entity status to false and deletion time to now</para>
    /// </summary>
    /// <param name="id">entity's id</param>
    /// <param name="deleterId">deleter id</param>
    Task DeleteAsync(Guid id, Guid deleterId);

    /// <summary>
    /// hard delete entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task HardDeleteAsync(Guid id);
}