using FtpDownloader.DataAccess.Interfaces.Repositories;
using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Mappers;
using FtpDownloader.Services.Interfaces.Models;

namespace FtpDownloader.Services.Models
{
    public class Journal : IJournal
    {
        //private string _pathToSettings = "..//Settings//appsettings.json";
        private readonly IJournalRepository _repository;
        private readonly DataLayerMapper _mapper;

        public event Action EntriesLoaded;
        public event Action EntryDeleted;
        public event Action EntryCreated;
        public event Action AllEntriesDeleted;
        public event Action<Exception> ExceptionThrowned;

        public Journal(IJournalRepository repository, DataLayerMapper mapper)
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

        public async Task<Entry[]> GetEntries()
        {
            var enries = new List<Entry>();
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

        public async Task CreateEntry(Entry entry)
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

        public async Task DeleteEntry(Entry entry)
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

        public Task CreateEntry(LogicLayerEntryDto entry)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntry(LogicLayerEntryDto entry)
        {
            throw new NotImplementedException();
        }

        Task<LogicLayerEntryDto[]> IJournal.GetEntries()
        {
            throw new NotImplementedException();
        }
    }
}
