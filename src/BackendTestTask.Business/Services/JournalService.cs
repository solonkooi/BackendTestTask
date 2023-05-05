using System.Text.Json;
using BackendTestTask.Business.Exceptions;
using BackendTestTask.Business.Interfaces;
using BackendTestTask.Business.Models;
using BackendTestTask.Core;
using BackendTestTask.Core.Interfaces;

namespace BackendTestTask.Business.Services;

public class JournalService : IJournalService
{
    private readonly IJournalsRepository _journalsRepository;

    public JournalService(IJournalsRepository journalsRepository)
    {
        _journalsRepository = journalsRepository;
    }

    public async Task SaveAsync(Exception e, string query, string body, Dictionary<string, string> headers)
    {
        var statusResponseText = new StatusResponseText
        {
            StackTrace = e.StackTrace,
            Message = e.Message,
            Body = body,
            Query = query,
            Headers = headers
        };
        var text = JsonSerializer.Serialize(statusResponseText);

        await _journalsRepository.CreateAsync(new Journal
        {
            created_date_utc = DateTime.UtcNow,
            event_id = e.HResult,
            text = text
        });
    }

    public async Task<Journal?> GetSingleAsync(long id)
    {
        var journal = await _journalsRepository.GetByIdAsync(id);
        if (journal == null)
        {
            throw new SecureException($"journal does not exist with id: {id}");
        }

        return journal;
    }

    public async Task<IEnumerable<Journal>?> GetRangeAsync(int skip, int take, JournalFilter? journalFilter)
    {
        var journal =
            await _journalsRepository.GetManyAsync(skip, take, journalFilter?.From, journalFilter?.To, journalFilter?.Search);
        if (journal == null)
        {
            throw new SecureException("journals does not exist with your parameters");
        }

        return journal;
    }
}