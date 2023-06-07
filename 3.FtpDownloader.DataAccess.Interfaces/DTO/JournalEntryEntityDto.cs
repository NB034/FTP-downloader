
namespace FtpDownloader.DataAccess.Interfaces.DTO
{
    public class JournalEntryEntityDto
    {
        public int Id { get; } = 0;
        public string RemotePath { get; set; } = string.Empty;
        public string LocalPath { get; set; } = string.Empty;
        public DateTime DownloadDate { get; set; }
        public int FileSize { get; set; }
        public bool WasSuccessful { get; set; }
        public List<TagEntityDto> TagEntities { get; set; } = new();



        public JournalEntryEntityDto(int id)
        {
            Id = id;
        }
    }
}
