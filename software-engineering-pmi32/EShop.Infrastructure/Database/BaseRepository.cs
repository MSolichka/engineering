using EShop.Application.Abstractions;
using EShop.Domain.Abstractions;
using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Database;

public class BaseRepository<TKey, TEntity, TContext> : 
    IRepository<TKey, TEntity>
    where TEntity : class, IBaseEntity<TKey> 
    where TContext : DbContext
{
    protected readonly TContext _dbContext;

    public BaseRepository(TContext dbContext)
    {
        _dbContext = dbContext;
    }   
    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id!.Equals(id)); 
    }

    public Task<List<TEntity>> GetAllBySpecificationAsync(Specification<TEntity> spec)
    {
        return ApplySpecification(spec).AsNoTracking().ToListAsync();
    }

    public Task<TEntity?> GetBySpecificationAsync(Specification<TEntity> spec)
    {
        return ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
    }


    public Task<List<TEntity>> GetAllAsync()
    {
        return _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public Task<bool> AnyAsync(TKey id)
    {
        return _dbContext.Set<TEntity>().AnyAsync(e => e.Id!.Equals(id));
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
        {
            return;
        }

        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<int> CountAsync()
    {
        return _dbContext.Set<TEntity>().CountAsync();
    }

    private IQueryable<TEntity> ApplySpecification(Specification<TEntity> specification)
    {
        return SpecificationEvaluator<TKey,TEntity>.GetQuery(_dbContext.Set<TEntity>(),specification);
    }
}