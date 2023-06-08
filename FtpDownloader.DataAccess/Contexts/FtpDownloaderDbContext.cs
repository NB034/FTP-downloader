using FtpDownloader.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FtpDownloader.DataAccess.Contexts
{
    public class FtpDownloaderDbContext : DbContext
    {
        public FtpDownloaderDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<EntryEntity> EntryEntities { get; set; }
        public virtual DbSet<TagEntity> TagEntities { get; set; }



        public static DbContextOptions CreateSQLiteOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FtpDownloaderDbContext>();
            return optionsBuilder.UseSqlite(connectionString).Options;
        }

        public static FtpDownloaderDbContext CreateSQLiteContext(string connectionString)
        {
            return new FtpDownloaderDbContext(CreateSQLiteOptions(connectionString));
        }
    }
}
