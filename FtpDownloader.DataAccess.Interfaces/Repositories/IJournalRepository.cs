using FtpDownloader.DataAccess.Interfaces.DTO;

namespace FtpDownloader.DataAccess.Interfaces.Repositories
{
    public interface IJournalRepository
    {
        List<EntryEntityDto> GetEntries();
        void CreateEntry(EntryEntityDto dto);
        void DeleteEntry(int id);
        void DeleteAllEntries();



        Task<List<EntryEntityDto>> GetEntriesAsync();
        Task CreateEntryAsync(EntryEntityDto dto);
        Task DeleteEntryAsync(int id);
        Task DeleteAllEntriesAsync();
    }
}
