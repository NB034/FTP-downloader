using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.Models
{
    public interface IJournal
    {
        event Action AllEntriesDeleted;
        event Action EntriesLoaded;
        event Action EntryCreated;
        event Action EntryDeleted;
        event Action<Exception> ExceptionThrowned;

        Task CreateEntry(LogicLayerEntryDto dto);
        Task DeleteAllEntries();
        Task DeleteEntry(LogicLayerEntryDto dto);
        Task<LogicLayerEntryDto[]> GetEntries();
    }
}