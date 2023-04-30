using FileDownloader.DataAccess.Contexts;
using FileDownloader.DataAccess.Entities;
using FileDownloader.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FileDownloader.Services.Mappers
{
    public class JournalModel
    {
        private string _pathToSettings = "..//Settings//appsettings.json";
        private FileDownloaderDbContext _context;
        private JournalEntryMapper _mapper;

        public event Action EntriesLoaded;
        public event Action EntryDeleted;
        public event Action EntryCreated;

        public JournalModel()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(_pathToSettings);

            var config = configBuilder.Build();
            string connectionString = config.GetConnectionString("connectionString");

            _context = FileDownloaderDbContext.CreateSQLiteContext(connectionString);

            _mapper = new JournalEntryMapper(_context);
        }

        public async Task<JournalEntryModel[]> GetEntries()
        {
            var entries = new List<JournalEntryModel>();
            var entities = await _context.EntryEntities.Include(nameof(JournalEntryEntity.TagEntities)).ToListAsync();

            foreach (var entity in entities)
            {
                entries.Add(_mapper.EntityToModel(entity));
            }
            EntriesLoaded?.Invoke();
            return entries.ToArray();
        }

        public async Task CreateEntry(JournalEntryModel entry)
        {
            var entity = _mapper.ModelToEntity(entry);

            await _context.EntryEntities.AddAsync(entity);
            await _context.SaveChangesAsync();

            EntryCreated?.Invoke();
        }

        public async Task DeleteEntry(JournalEntryModel entry)
        {
            var entity = await _context.EntryEntities.Include(nameof(JournalEntryEntity.TagEntities))
                .FirstOrDefaultAsync(e => e.Id == entry.Id);
            if (entity == null) throw new ArgumentException("Entry does not exist");
            var tags = entity.TagEntities.ToList();

            _context.EntryEntities.Remove(entity);
            tags.ForEach(t => DeleteTagIfNotUsed(t));
            await _context.SaveChangesAsync();

            EntryDeleted?.Invoke();
        }

        private void DeleteTagIfNotUsed(TagEntity tag)
        {
            if (tag.EntryEntities.Count == 0)
            {
                _context.TagEntities.Remove(tag);
            }
        }
    }
}
