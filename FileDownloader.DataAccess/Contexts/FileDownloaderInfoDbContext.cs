using FileDownloader.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileDownloader.DataAccess.Contexts
{
    internal class FileDownloaderInfoDbContext : DbContext
    {
        public DbSet<DownloadedItemInfo> DownloadedItems { get; set; }
        public DbSet<UnfinishedDownloadInfo> UnfinishedDownloads { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FileDownloaderInfo.db");
        }
    }
}
