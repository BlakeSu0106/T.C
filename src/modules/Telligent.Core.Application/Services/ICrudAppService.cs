using System.Linq.Expressions;
using Telligent.Core.Application.DataTransferObjects;
using Telligent.Core.Domain.Entities;
using Telligent.Core.Infrastructure.Services;

namespace Telligent.Core.Application.Services;

public interface ICrudAppService<TEntity, TReadDto, TCreateDto, in TUpdateDto> : IAppService
    where TEntity : Entity
    where TReadDto : EntityDto
    where TCreateDto : EntityDto
    where TUpdateDto : EntityDto
{
    /// <summary>
    /// get data transfer object by id
    /// </summary>
    /// <param name="id">guid primary key</param>
    /// <returns>entity</returns>
    Task<TReadDto> GetAsync(Guid id);

    /// <summary>
    /// get first data transfer object by linq query
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TReadDto> GetAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TReadDto> GetWithDeletionAsync(Guid id);

    /// <summary>
    /// get data transfer object list by linq query
    /// </summary>
    /// <param name="predicate">linq query</param>
    /// <returns>entities</returns>
    Task<IList<TReadDto>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// get all activated data transfer object list by tenant id
    /// will throw exception when entity don't have tenant id like Account
    /// </summary>
    /// <returns>entities</returns>
    Task<IList<TReadDto>> GetAllAsync();

    /// <summary>
    /// insert entity into database
    /// id will be auto generate a sequence guid by SequentialGuidValueGenerator
    /// </summary>
    /// <param name="dto">data transfer object</param>
    /// <returns>entity with primary key</returns>
    Task<TReadDto> CreateAsync(TCreateDto dto);

    /// <summary>
    /// insert multiple entities into database with transaction
    /// id will be auto generate a sequence guid by SequentialGuidValueGenerator
    /// </summary>
    /// <param name="dtos">data transfer objects</param>
    /// <returns>entities with primary key</returns>
    Task<IList<TReadDto>> CreateAsync(IList<TCreateDto> dtos);

    /// <summary>
    /// update entity by id
    /// </summary>
    /// <param name="dto">data transfer object</param>
    /// <returns>update result, true will be update success, otherwise false</returns>
    Task<bool> UpdateAsync(TUpdateDto dto);

    /// <summary>
    /// soft delete entity
    /// soft delete means keep the data instance but update activate status to 0
    /// </summary>
    /// <param name="id">entity's id</param>
    /// <returns>delete result, true will be delete success, otherwise false</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// delete entity by id
    /// </summary>
    /// <param name="id">entity's id</param>
    /// <returns>delete result, true will be delete success, otherwise false</returns>
    Task<bool> HardDeleteAsync(Guid id);

    /// <summary>
    /// set addition properties for data transfer object. used in GetAsync, GetListAsync, GetAllAsync methods
    /// <para>it's a template pattern in get methods</para>
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<TReadDto> SetAdditionPropertiesAsync(TReadDto dto);
}