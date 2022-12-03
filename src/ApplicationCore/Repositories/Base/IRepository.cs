using ApplicationCore.Entities.Base;
using System.Linq.Expressions;

namespace ApplicationCore.Repositories.Base
{
    public interface IRepository<T> where T : BaseObject
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync();
    }
}
