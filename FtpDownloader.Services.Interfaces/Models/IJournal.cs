using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Models.JournalModels
{
    public interface IJournal
    {
        event Action AllEntriesDeleted;
        event Action EntriesLoaded;
        event Action EntryCreated;
        event Action EntryDeleted;
        event Action<Exception> ExceptionThrowned;

        Task CreateEntry(EntryDto entry);
        Task DeleteAllEntries();
        Task DeleteEntry(EntryDto entry);
        Task<EntryDto[]> GetEntries();
    }
}