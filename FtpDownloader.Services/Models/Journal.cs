using FtpDownloader.DataAccess.Interfaces.Repositories;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Mappers;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;

namespace FtpDownloader.Services.Models
{
    public class Journal : IJournal
    {
        private readonly IJournalRepository _repository;
        private readonly DataLayerMapper _dataLayerMapper;
        private readonly LogicLayerMapper _logicLayerMapper;

        public event EventHandler EntriesLoaded;
        public event EventHandler EntryDeleted;
        public event EventHandler EntryCreated;
        public event EventHandler AllEntriesDeleted;
        public event EventHandler<ExceptionThrownedEventArgs> ExceptionThrowned;

        public Journal(IJournalRepository repository, DataLayerMapper dataLayerMapper, LogicLayerMapper logicLayerMapper)
        {
            _repository = repository;
            _dataLayerMapper = dataLayerMapper;
            _logicLayerMapper = logicLayerMapper;
        }

        public async Task DeleteAllEntries()
        {
            await _repository.DeleteAllEntriesAsync();
            AllEntriesDeleted?.Invoke(this, EventArgs.Empty);
        }

        public async Task CreateEntry(LogicLayerEntryDto dto)
        {
            try
            {
                var entry = _logicLayerMapper.DtoToEntry(dto);
                var dataDto = _dataLayerMapper.EntryToDto(entry);

                await _repository.CreateEntryAsync(dataDto);
                EntryCreated?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ExceptionThrowned?.Invoke(this, new ExceptionThrownedEventArgs(ex));
            }
        }

        public async Task DeleteEntry(LogicLayerEntryDto entry)
        {
            try
            {
                var dto = (await _repository.GetEntriesAsync()).FirstOrDefault(e => e.Id == entry.Id);
                if (dto == null) throw new ArgumentException("Entry does not exist");

                _repository.DeleteEntry(dto.Id);

                EntryDeleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ExceptionThrowned?.Invoke(this, new ExceptionThrownedEventArgs(ex));
            }
        }

        public async Task<LogicLayerEntryDto[]> GetEntries()
        {
            var entries = new List<LogicLayerEntryDto>();
            try
            {
                var dtos = await _repository.GetEntriesAsync();

                foreach (var dto in dtos)
                {
                    var entry = _dataLayerMapper.DtoToEntry(dto);
                    var logicDto = _logicLayerMapper.EntryToDto(entry);
                    entries.Add(logicDto);
                }
                EntriesLoaded?.Invoke(this, EventArgs.Empty);
                return entries.ToArray();
            }
            catch (Exception ex)
            {
                ExceptionThrowned?.Invoke(this, new ExceptionThrownedEventArgs(ex));
            }
            return entries.ToArray();
        }
    }
}
