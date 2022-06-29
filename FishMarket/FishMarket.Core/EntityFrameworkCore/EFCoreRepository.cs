using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FishMarket.Core.EntityFrameworkCore
{
    public class EFRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<int> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;

                return await context.SaveChangesAsync();
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            using (var context = new TContext())
            {     
                context.RemoveRange(entities);
            }
        }


        public async Task<int> Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<List<TEntity>> GetListAsNoTracking(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? await context.Set<TEntity>().AsNoTracking().ToListAsync() : await context.Set<TEntity>().Where(filter).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? await context.Set<TEntity>().ToListAsync() : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async Task<TEntity> GetById(Guid id)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }


        public async Task<int> Delete(Guid id)
        {
            using (var context = new TContext())
            {
                var entity = context.Set<TEntity>().SingleOrDefault();

                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;

                return await context.SaveChangesAsync();

            }
        }
    }
}
