using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;

namespace FtpDownloader.Services.Interfaces.Models
{
    public interface IJournal
    {
        event EventHandler AllEntriesDeleted;
        event EventHandler EntriesLoaded;
        event EventHandler EntryCreated;
        event EventHandler EntryDeleted;
        event EventHandler<ExceptionThrownedEventArgs> ExceptionThrowned;

        Task CreateEntry(LogicLayerEntryDto dto);
        Task DeleteAllEntries();
        Task DeleteEntry(LogicLayerEntryDto dto);
        Task<LogicLayerEntryDto[]> GetEntries();
    }
}