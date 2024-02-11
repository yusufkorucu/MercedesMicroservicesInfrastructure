using IdentityService.Api.Infrastructure.Context;
using IdentityService.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdentityService.Api.Core.Application.Repository.Base
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IdentityApiDbContext Context;

        public RepositoryBase(IdentityApiDbContext context)
            => this.Context = context;

        public async Task<Guid> AddAsync(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.IsActive = true;
            entity.IsDeleted = false;

            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.CreateDate = DateTime.Now;
                x.IsActive = true;
                x.IsDeleted = false;
            });

            await Context.Set<TEntity>().AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
            => Context.Set<TEntity>().Where(predicate);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await Context.Set<TEntity>().ToListAsync();

        public ValueTask<TEntity> GetByIdAsync(Guid id)
            => Context.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
            entity.IsActive = false;
            entity.IsDeleted = true;

            Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
        }

        public void HardRemoveRange(List<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            Context.SaveChanges();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => Context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public Task Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;

            Context.ChangeTracker.Clear();
            Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();

            return Task.FromResult(entity);
        }

        public async Task UpdateRangeAsync(List<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
            await Context.SaveChangesAsync();
        }

        public async Task<Guid> AddAsyncWithoutCreateDate(TEntity entity)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;

            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
