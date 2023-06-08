
namespace FtpDownloader.Services.Models.JournalModels
{
    public interface IJournal
    {
        event Action AllEntriesDeleted;
        event Action EntriesLoaded;
        event Action EntryCreated;
        event Action EntryDeleted;
        event Action<Exception> ExceptionThrowned;

        Task CreateEntry(JournalEntryModel entry);
        Task DeleteAllEntries();
        Task DeleteEntry(JournalEntryModel entry);
        Task<JournalEntryModel[]> GetEntries();
    }
}