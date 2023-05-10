namespace BackendTestTask.Core.Interfaces;

public interface IJournalsRepository : IBaseRepository<Journal>
{
    Task<IEnumerable<Journal>> GetManyAsync(int skip, int take, DateTime? from, DateTime? to, string? search);
}