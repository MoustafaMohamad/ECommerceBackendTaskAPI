using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace ECommerceBackendTaskAPI.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly Context _context;
        public Repository(Context context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity.ID;
        }

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
           return _context.Set<T>().Where<T>(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>(); 
        }
        
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(a =>a.ID == id);
        }

        public void UpdateIncluded(T entity, params string[] updatedProperties)
        {
            T local = _context.Set<T>().Local.FirstOrDefault(x => x.ID == entity.ID);

            EntityEntry entityEntry;

            if (local is null)
            {
                entityEntry = _context.Entry(entity);
            }
            else
            {
                entityEntry = _context.ChangeTracker.Entries<T>().FirstOrDefault(x => x.Entity.ID == entity.ID);
            }

            foreach (var property in entityEntry.Properties)
            {
                if (updatedProperties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;
                }
            }
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
             UpdateIncluded(entity , nameof(entity.IsDeleted));
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

      
    }
}
