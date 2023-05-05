namespace BackendTestTask.Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id);
    Task<bool> UpdateAsync(T entity);
    Task<long?> CreateAsync(T entity);
    Task<bool> DeleteAsync(long id);

    Task<IEnumerable<T>> GetAll();
}