using FtpDownloader.DataAccess.Interfaces.DTO;

namespace FtpDownloader.DataAccess.Interfaces.Repositories
{
    public interface IJournalRepository
    {
        List<JournalEntryEntityDto> GetEntries();
        void CreateEntry(JournalEntryEntityDto dto);
        void DeleteEntry(int id);
        void DeleteAllEntries();



        Task<List<JournalEntryEntityDto>> GetEntriesAsync();
        Task CreateEntryAsync(JournalEntryEntityDto dto);
        Task DeleteEntryAsync(int id);
        Task DeleteAllEntriesAsync();
    }
}
