using System.Linq.Expressions;

namespace FishMarket.Core
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<T> Get(Expression<Func<T, bool>> filter = null);

        Task<List<T>> GetListAsNoTracking(Expression<Func<T, bool>> filter = null);

        Task<List<T>> GetList(Expression<Func<T, bool>> filter = null);

        Task<T> GetById(Guid id);

        Task<int> Add(T entity);

        Task<int> Update(T entity);

        Task<int> Delete(T entity);

        Task<int> Delete(Guid id);
    }
}
