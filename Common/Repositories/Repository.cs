using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.Repositories
{
    public abstract class Repository<T>: IRepository<T> where T: class
    {
        protected readonly DbSet<T> _context;

        protected Repository(DbSet<T> context)
        {
            _context = context;
        }

        public async Task<EntityEntry<T>> CreateAsync(T obj)
        {
            return await _context.AddAsync(obj);
        }

        public void Update(T obj)
        {
            _context.Update(obj);
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _context.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.FindAsync(id);
        }

        public void Delete(T obj)
        {
            throw new NotImplementedException();
        }
    }
}