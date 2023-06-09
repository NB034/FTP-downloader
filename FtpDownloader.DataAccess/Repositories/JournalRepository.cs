using FtpDownloader.DataAccess.Contexts;
using FtpDownloader.DataAccess.Entities;
using FtpDownloader.DataAccess.Interfaces.DTO;
using FtpDownloader.DataAccess.Interfaces.Repositories;
using FtpDownloader.DataAccess.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FtpDownloader.DataAccess.Repositories
{
    public partial class JournalRepository : IJournalRepository
    {
        private readonly FtpDownloaderDbContext _context;
        private readonly DataLayerMapper _mapper;

        public JournalRepository(FtpDownloaderDbContext context, DataLayerMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }


    // Sync methods
    public partial class JournalRepository
    {
        public void CreateEntry(DataLayerEntryDto dto)
        {
            var entity = _mapper.DtoToEntity(dto);

            foreach(var tag in dto.Tags)
            {
                entity.TagEntities.Add(GetOrCreate(tag));
            }

            _context.EntryEntities.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteAllEntries()
        {
            _context.EntryEntities.RemoveRange(_context.EntryEntities);
            RemoveUnnecessaryTags();
            _context.SaveChanges();
        }

        public void DeleteEntry(int id)
        {
            _context.EntryEntities.Remove(_context.EntryEntities.Where(e => e.Id == id).FirstOrDefault());
            RemoveUnnecessaryTags();
            _context.SaveChanges();
        }

        public List<DataLayerEntryDto> GetEntries()
        {
            var entities = _context.EntryEntities.Include(nameof(EntryEntity.TagEntities)).ToList();
            var dtos = new List<DataLayerEntryDto>();
            foreach (var entity in entities)
            {
                dtos.Add(_mapper.EntityToDto(entity));
            }
            return dtos;
        }

        private TagEntity GetOrCreate(string tag)
        {
            var entity = _context.TagEntities.Where(t => t.Name == tag).FirstOrDefault();
            if(entity != null) return entity;
            
            entity = _context.TagEntities.Add(new TagEntity() { Name = tag }).Entity;
            _context.SaveChanges();
            return entity;
        }

        private void RemoveUnnecessaryTags()
        {
            foreach(var tag in _context.TagEntities)
            {
                if(!tag.EntryEntities.Any()) _context.TagEntities.Remove(tag);
            }
            _context.SaveChanges();
        }
    }




    // Async methods
    public partial class JournalRepository
    {
        public async Task CreateEntryAsync(DataLayerEntryDto dto)
        {
            var entryEntity = _mapper.DtoToEntity(dto);
            entryEntity = (await _context.EntryEntities.AddAsync(entryEntity)).Entity;

            foreach (var tag in dto.Tags)
            {
                var tagEntity = await GetOrCreateAsync(tag);
                entryEntity.TagEntities.Add(tagEntity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllEntriesAsync()
        {
            _context.EntryEntities.RemoveRange(await _context.EntryEntities.ToListAsync());
            await RemoveUnnecessaryTagsAsync();
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntryAsync(int id)
        {
            _context.EntryEntities.Remove(await _context.EntryEntities.Where(e => e.Id == id).FirstOrDefaultAsync());
            await RemoveUnnecessaryTagsAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<List<DataLayerEntryDto>> GetEntriesAsync()
        {
            var entities = await _context.EntryEntities.Include(nameof(EntryEntity.TagEntities)).ToListAsync();
            var dtos = new List<DataLayerEntryDto>();
            foreach (var entity in entities)
            {
                dtos.Add(_mapper.EntityToDto(entity));
            }
            return dtos;
        }

        private async Task<TagEntity> GetOrCreateAsync(string tag)
        {
            var entity = await _context.TagEntities.Where(t => t.Name == tag).FirstOrDefaultAsync();
            if (entity != null) return entity;

            entity = (await _context.TagEntities.AddAsync(new TagEntity() { Name = tag })).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        private async Task RemoveUnnecessaryTagsAsync()
        {
            foreach (var tag in await _context.TagEntities.ToListAsync())
            {
                if (!tag.EntryEntities.Any()) _context.TagEntities.Remove(tag);
            }
            await _context.SaveChangesAsync();
        }
    }
}
