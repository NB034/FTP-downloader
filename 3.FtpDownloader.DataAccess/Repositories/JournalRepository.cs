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

        public void CreateEntry(JournalEntryEntityDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task CreateEntryAsync(JournalEntryEntityDto dto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllEntries()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAllEntriesAsync()
        {
            throw new NotImplementedException();
        }

        public void DeleteEntry(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteEntryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<JournalEntryEntityDto> GetEntries()
        {
            var entities = _context.EntryEntities.Include(nameof(JournalEntryEntity.TagEntities)).ToList();
            var dtos = new List<JournalEntryEntityDto>();
            foreach (var entity in entities)
            {
                //dtos.Add();
            }
            return dtos;
        }

        public async Task<List<JournalEntryEntityDto>> GetEntriesAsync()
        {
            var entities = await _context.EntryEntities.Include(nameof(JournalEntryEntity.TagEntities)).ToListAsync();
            var dtos = new List<JournalEntryEntityDto>();
            foreach (var entity in entities)
            {
                //dtos.Add();
            }
            return dtos;
        }
    }
}
