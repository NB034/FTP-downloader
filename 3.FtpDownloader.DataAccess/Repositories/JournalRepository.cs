using FtpDownloader.DataAccess.Contexts;
using FtpDownloader.DataAccess.Entities;
using FtpDownloader.DataAccess.Interfaces.DTO;
using FtpDownloader.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FtpDownloader.DataAccess.Repositories
{
    internal class JournalRepository : IJournalRepository
    {
        private readonly FtpDownloaderDbContext _context;

        public JournalRepository(FtpDownloaderDbContext context)
        {
            _context = context;
        }

        public void CreateEntry()
        {

        }

        public void DeleteAllEntries()
        {

        }

        public void DeleteEntry(int id)
        {

        }

        public List<JournalEntryEntityDto> GetEntries()
        {
            var entities = _context.EntryEntities.Include(nameof(JournalEntryEntity.TagEntities)).ToList();
            var dtos = new List<JournalEntryEntityDto>();
            foreach(var entity in entities)
            {
                //dtos.Add();
            }
            return dtos;
        }
    }
}
