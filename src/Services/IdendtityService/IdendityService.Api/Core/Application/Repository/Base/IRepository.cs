using IdentityService.Api.Infrastructure.Data;
using System.Linq.Expressions;

namespace IdentityService.Api.Core.Application.Repository.Base
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        ValueTask<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<Guid> AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void Remove(TEntity entity);
        void HardRemoveRange(List<TEntity> entities);
        Task Update(TEntity entity);
        Task UpdateRangeAsync(List<TEntity> entities);
        Task<Guid> AddAsyncWithoutCreateDate(TEntity entity);
    }
}
