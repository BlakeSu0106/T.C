using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.DataTransferObjects;
using Telligent.Core.Domain.Auth;
using Telligent.Core.Domain.Entities;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Generators;
using Telligent.Core.Infrastructure.Profiles;

namespace Telligent.Core.Application.Services;

public abstract class
    CrudAppService<TEntity, TReadDto, TCreateDto, TUpdateDto> : ICrudAppService<TEntity, TReadDto, TCreateDto,
        TUpdateDto>
    where TEntity : Entity
    where TReadDto : EntityDto
    where TCreateDto : EntityDto
    where TUpdateDto : EntityDto
{
    protected readonly IHttpContextAccessor HttpContextAccessor;
    protected readonly IMapper Mapper;
    protected readonly IRepository<TEntity> Repository;
    public UserProfile Payload;
    public string Token;

    protected CrudAppService(IRepository<TEntity> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        Repository = repository;
        Mapper = mapper;
        HttpContextAccessor = httpContextAccessor;

        Payload = ProfileHelper.GetProfile(httpContextAccessor.HttpContext, out Token) ?? new UserProfile();
    }

    /// <summary>
    /// get data transfer object by id
    /// <para>entity status = true</para>
    /// </summary>
    /// <param name="id">guid primary key</param>
    /// <returns>entity</returns>
    public virtual async Task<TReadDto> GetAsync(Guid id)
    {
        return await SetAdditionPropertiesAsync(Mapper.Map<TReadDto>(await Repository.GetAsync(id)));
    }

    /// <summary>
    /// get first data transfer object by linq query
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TReadDto> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await SetAdditionPropertiesAsync(Mapper.Map<TReadDto>(await Repository.GetAsync(predicate)));
    }

    /// <summary>
    /// get data transfer object by id without check entity status (will return dto even it was deleted)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TReadDto> GetWithDeletionAsync(Guid id)
    {
        return await SetAdditionPropertiesAsync(Mapper.Map<TReadDto>(
            await Repository.GetAsync(r => r.TenantId.Equals(Payload.TenantId) && r.Id.Equals(id))));
    }

    /// <summary>
    /// get all activated data transfer object list by tenant id
    /// <para>will throw exception when entity don't have tenant id like Account</para>
    /// </summary>
    /// <returns>entities</returns>
    public virtual async Task<IList<TReadDto>> GetAllAsync()
    {
        return await GetListAsync(entity => entity.TenantId.Equals(Payload.TenantId) && entity.EntityStatus);
    }

    /// <summary>
    /// get data transfer object list by linq query
    /// </summary>
    /// <param name="predicate">linq query</param>
    /// <returns>entities</returns>
    public virtual async Task<IList<TReadDto>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = new List<TReadDto>();
        var dtos = Mapper.Map<IList<TReadDto>>(await Repository.GetListAsync(predicate));

        foreach (var dto in dtos) result.Add(await SetAdditionPropertiesAsync(dto));

        return result;
    }

    /// <summary>
    /// set additional properties in get method
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public virtual Task<TReadDto> SetAdditionPropertiesAsync(TReadDto dto)
    {
        return Task.FromResult(dto);
    }

    /// <summary>
    /// insert entity into database
    /// id will be auto generate a sequence guid by SequentialGuidValueGenerator
    /// </summary>
    /// <param name="dto">data transfer object</param>
    /// <returns>entity with primary key</returns>
    public virtual async Task<TReadDto> CreateAsync(TCreateDto dto)
    {
        var entity = Mapper.Map<TEntity>(dto);

        entity.Id = SequentialGuidGenerator.Instance.GetGuid();
        entity.TenantId = Payload.TenantId;
        entity.CreatorId = Payload.UserId;

        await Repository.CreateAsync(entity);
        await Repository.SaveChangesAsync();

        return await GetAsync(entity.Id);
    }

    /// <summary>
    /// insert multiple entities into database with transaction
    /// id will be auto generate a sequence guid by SequentialGuidValueGenerator
    /// </summary>
    /// <param name="dtos">data transfer objects</param>
    /// <returns>entities with primary key</returns>
    public virtual async Task<IList<TReadDto>> CreateAsync(IList<TCreateDto> dtos)
    {
        var entities = Mapper.Map<IList<TEntity>>(dtos);
        var result = new List<TReadDto>();

        foreach (var entity in entities)
        {
            entity.Id = SequentialGuidGenerator.Instance.GetGuid();
            entity.TenantId = Payload.TenantId;
            entity.CreatorId = Payload.UserId;

            result.Add(await GetAsync(entity.Id));
        }

        await Repository.CreateAsync(entities);
        await Repository.SaveChangesAsync();

        return result;
    }

    /// <summary>
    /// update entity by id
    /// </summary>
    /// <param name="dto">data transfer object</param>
    /// <returns>update result, true will be update success, otherwise false</returns>
    public virtual async Task<bool> UpdateAsync(TUpdateDto dto)
    {
        var entity = Mapper.Map(dto, await Repository.GetAsync(dto.Id));

        if (entity == null) return false;

        entity.ModifierId = Payload.UserId;

        Repository.Update(entity);

        return await Repository.SaveChangesAsync() == 1;
    }

    /// <summary>
    /// soft delete entity
    /// soft delete means keep the data instance but update activate status to 0
    /// </summary>
    /// <param name="id">entity's id</param>
    /// <returns>delete result, true will be delete success, otherwise false</returns>
    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id, Payload.UserId);
        return await Repository.SaveChangesAsync() == 1;
    }

    /// <summary>
    /// delete entity by id
    /// </summary>
    /// <param name="id">entity's id</param>
    /// <returns>delete result, true will be delete success, otherwise false</returns>
    public virtual async Task<bool> HardDeleteAsync(Guid id)
    {
        await Repository.HardDeleteAsync(id);
        return await Repository.SaveChangesAsync() == 1;
    }
}