
namespace FileDownloader.DataAccess.Entities
{
    public class JournalEntryEntity
    {
        public int Id { get; set; }
        public string RemotePath { get; set; } = string.Empty;
        public string LocalPath { get; set; } = string.Empty;
        public DateTime DownloadDate { get; set; }
        public int FileSize { get; set; }
        public bool WasSuccessful { get; set; }  

        public virtual ICollection<TagEntity> TagEntities { get; set; }
    }
}
