using System.Linq.Expressions;

namespace ECommerceBackendTaskAPI.Data.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<int> AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        void UpdateIncluded(T entity, params string[] updatedProperties);
        Task AddRangeAsync(IEnumerable<T> entities); 
        Task DeleteAsync(T entity); 
       
    }
}
