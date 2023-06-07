using FtpDownloader.DataAccess.Interfaces.DTO;

namespace FtpDownloader.DataAccess.Interfaces.Repositories
{
    public interface IJournalRepository
    {
        List<JournalEntryDto> GetEntries();
        void CreateEntry();
        void DeleteEntry(int id);
        void DeleteAllEntries();
    }
}
