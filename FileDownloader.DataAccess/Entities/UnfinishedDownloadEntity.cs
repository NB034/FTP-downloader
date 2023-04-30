
namespace FileDownloader.DataAccess.Entities
{
    public class UnfinishedDownloadEntity
    {
        public int Id { get; set; }
        public string RemotePath { get; set; } = string.Empty;
        public string LocalDirectory { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public DateOnly FileLastChangeDate { get; set; }
        public int DownloadedBytes { get; set; }
        
        public virtual ICollection<TagEntity> TagEntities { get; set; }
    }
}
