
namespace FtpDownloader.DataAccess.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<EntryEntity> EntryEntities { get; set; } = new HashSet<EntryEntity>();
    }
}
