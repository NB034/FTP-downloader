using FtpDownloader.DataAccess.Interfaces.DTO;

namespace FtpDownloader.DataAccess.Interfaces.Repositories
{
    public interface IJournalRepository
    {
        List<DataLayerEntryDto> GetEntries();
        void CreateEntry(DataLayerEntryDto dto);
        void DeleteEntry(int id);
        void DeleteAllEntries();



        Task<List<DataLayerEntryDto>> GetEntriesAsync();
        Task CreateEntryAsync(DataLayerEntryDto dto);
        Task DeleteEntryAsync(int id);
        Task DeleteAllEntriesAsync();
    }
}
