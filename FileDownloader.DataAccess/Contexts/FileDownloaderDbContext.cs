using FileDownloader.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileDownloader.DataAccess.Contexts
{
    public class FileDownloaderDbContext : DbContext
    {
        public FileDownloaderDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<JournalEntryEntity> EntryEntities { get; set; }
        public virtual DbSet<TagEntity> TagEntities { get; set; }
        public virtual DbSet<UnfinishedDownloadEntity> UnfinishedDownloadEntities { get; set; }



        public static DbContextOptions CreateSQLiteOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FileDownloaderDbContext>();
            return optionsBuilder.UseSqlite(connectionString).Options;
        }

        public static FileDownloaderDbContext CreateSQLiteContext(string connectionString)
        {
            return new FileDownloaderDbContext(CreateSQLiteOptions(connectionString));
        }
    }
}
