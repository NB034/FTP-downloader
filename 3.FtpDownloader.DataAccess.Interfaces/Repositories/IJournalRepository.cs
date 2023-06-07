using FtpDownloader.DataAccess.Interfaces.DTO;

namespace FtpDownloader.DataAccess.Interfaces.Repositories
{
    public interface IJournalRepository
    {
        List<JournalEntryEntityDto> GetEntries();
        void CreateEntry();
        void DeleteEntry(int id);
        void DeleteAllEntries();
    }
}
