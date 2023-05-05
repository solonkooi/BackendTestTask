using BackendTestTask.Core.Interfaces;
using Dapper.Contrib.Extensions;

namespace BackendTestTask.Core.Repository;

public class JournalsRepository : BaseRepository<Journal>, IJournalsRepository
{
    public JournalsRepository(DapperContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Journal>> GetManyAsync(int skip, int take, DateTime? from, DateTime? to, string? search)
    {
        using var connection = _context.CreateConnection();
        var journals = await connection.GetAllAsync<Journal>();

        return journals
            .Where(x => (from != null && x.created_date_utc >= from)
                        || (to != null && x.created_date_utc <= to)
                        || (search != null && x.text.Contains(search)))   
            .Skip(skip)
            .Take(take);
    }
}