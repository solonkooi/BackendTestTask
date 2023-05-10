using BackendTestTask.Core.Interfaces;
using Dapper.Contrib.Extensions;

namespace BackendTestTask.Core.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    public readonly DapperContext _context;

    public BaseRepository(DapperContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        using var connection = _context.CreateConnection();
        return await connection.GetAsync<T>(id);
    }

    public async Task<long?> CreateAsync(T entity)
    {
        using var connection = _context.CreateConnection();
        return await connection.InsertAsync<T>(entity);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using var connection = _context.CreateConnection();

        var entity = await connection.GetAsync<T>(id);
        
        return await connection.DeleteAsync<T>(entity);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        using var connection = _context.CreateConnection();
        return await connection.GetAllAsync<T>();
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        using var connection = _context.CreateConnection();
        return await connection.UpdateAsync(entity);
    }
}