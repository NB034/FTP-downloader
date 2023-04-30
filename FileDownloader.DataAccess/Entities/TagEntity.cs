
namespace FileDownloader.DataAccess.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<JournalEntryEntity> EntryEntities { get; set; }
        public virtual ICollection<UnfinishedDownloadEntity> UnfinishedDownloadEntities { get; set;}
    }
}
