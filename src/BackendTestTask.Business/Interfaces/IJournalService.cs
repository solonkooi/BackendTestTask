using BackendTestTask.Business.Models;
using BackendTestTask.Core;

namespace BackendTestTask.Business.Interfaces;

public interface IJournalService
{
    Task SaveAsync(Exception e, string query, string body, Dictionary<string, string> headers);
    Task<Journal?> GetSingleAsync(long id);
    Task<IEnumerable<Journal>?> GetRangeAsync(int skip, int take, JournalFilter? journalFilter);
}