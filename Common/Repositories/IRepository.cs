using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.Repositories;

public interface IRepository<T> where T : class
{
    public Task<EntityEntry<T>> CreateAsync(T obj);
    public void Update(T obj);
    public Task<IEnumerable<T?>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public void Delete(T obj);
}