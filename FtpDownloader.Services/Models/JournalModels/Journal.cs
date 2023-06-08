using FtpDownloader.DataAccess.Interfaces.Repositories;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.Models.JournalModels
{
    public class Journal : IJournal
    {
        //private string _pathToSettings = "..//Settings//appsettings.json";
        private readonly IJournalRepository _repository;
        private readonly EntryToDtoMapper _mapper;

        public event Action EntriesLoaded;
        public event Action EntryDeleted;
        public event Action EntryCreated;
        public event Action AllEntriesDeleted;
        public event Action<Exception> ExceptionThrowned;

        public Journal(IJournalRepository repository, EntryToDtoMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            //try
            //{
            //    var configBuilder = new ConfigurationBuilder();
            //    configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            //    configBuilder.AddJsonFile(_pathToSettings);

            //    var config = configBuilder.Build();
            //    string connectionString = config.GetConnectionString("connectionString");

            //    _context = FtpDownloaderDbContext.CreateSQLiteContext(connectionString);

            //    _mapper = new JournalEntryMapper(_context);
            //}
            //catch (Exception ex)
            //{
            //    ExceptionThrowned?.Invoke(ex);
            //}
        }

        public async Task<JournalEntryModel[]> GetEntries()
        {
            var enries = new List<JournalEntryModel>();
            try
            {
                var dtos = await _repository.GetEntriesAsync();

                foreach (var dto in dtos)
                {
                    enries.Add(_mapper.DtoToEntry(dto));
                }
                EntriesLoaded?.Invoke();
                return enries.ToArray();
            }
            catch (Exception ex)
            {
                ExceptionThrowned?.Invoke(ex);
            }
            return enries.ToArray();
        }

        public async Task CreateEntry(JournalEntryModel entry)
        {
            try
            {
                var dto = _mapper.EntryToDto(entry);

                await _repository.CreateEntryAsync(dto);

                EntryCreated?.Invoke();
            }
            catch (Exception ex)
            {
                ExceptionThrowned?.Invoke(ex);
            }
        }

        public async Task DeleteEntry(JournalEntryModel entry)
        {
            try
            {
                var dto = (await _repository.GetEntriesAsync()).FirstOrDefault(e => e.Id == entry.Id);
                if (dto == null) throw new ArgumentException("Entry does not exist");

                _repository.DeleteEntry(dto.Id);

                EntryDeleted?.Invoke();
            }
            catch (Exception ex)
            {
                ExceptionThrowned?.Invoke(ex);
            }
        }

        public async Task DeleteAllEntries()
        {
            await _repository.DeleteAllEntriesAsync();
            AllEntriesDeleted?.Invoke();
        }
    }
}
