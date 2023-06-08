using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.TestModels
{
    public class TestJournal : IJournal
    {
        private List<Entry> _journalEntries;
        private readonly LogicLayerMapper _mapper;

        public TestJournal(LogicLayerMapper mapper)
        {
            _journalEntries = new List<Entry>();
            _mapper = mapper;

            Seed();
        }

        public event Action AllEntriesDeleted;
        public event Action EntriesLoaded;
        public event Action EntryCreated;
        public event Action EntryDeleted;
        public event Action<Exception> ExceptionThrowned;




        public async Task DeleteAllEntries()
        {
            await Task.Delay(2000);
            _journalEntries.Clear();
            AllEntriesDeleted?.Invoke();
        }

        public async Task CreateEntry(LogicLayerEntryDto dto)
        {
            await Task.Delay(2000);
            _journalEntries.Add(_mapper.DtoToEntry(dto));
            EntryCreated?.Invoke();
        }

        public async Task DeleteEntry(LogicLayerEntryDto dto)
        {
            await Task.Delay(2000);
            _journalEntries.Remove(_mapper.DtoToEntry(dto));
            EntryDeleted?.Invoke();
        }

        public async Task<LogicLayerEntryDto[]> GetEntries()
        {
            await Task.Delay(2000);
            var dtos = new List<LogicLayerEntryDto>();
            foreach (var entry in _journalEntries)
            {
                dtos.Add(_mapper.EntryToDto(entry));
            }
            EntriesLoaded?.Invoke();
            return dtos.ToArray();
        }




        private void Seed()
        {
            _journalEntries.AddRange(new[]
                {
                    new Entry
                    {
                        DownloadDate = DateTime.Now,
                        FileSize = 10_000,
                        Id = 1,
                        LocalPath = "Some/Directory/NiceGame",
                        RemotePath = "Some/Remote/Directory/CoolGameOnServer",
                        Tags = new List<string> { "anime", "girls", "18+", "UWU", "lol" },
                        WasSuccessful = true
                    },
                    new Entry
                    {
                        DownloadDate = DateTime.Now,
                        FileSize = 10_000,
                        Id = 1,
                        LocalPath = "Some/Directory/CoolGame",
                        RemotePath = "Some/Remote/Directory/CoolGameOnServer",
                        Tags = new List<string> { "shooter", "sci-fi", "18+", "bla", "random tag idk" },
                        WasSuccessful = false
                    },
                    new Entry
                    {
                        DownloadDate = DateTime.Now,
                        FileSize = 10_000,
                        Id = 1,
                        LocalPath = "Some/Directory/ScaryGame",
                        RemotePath = "Some/Remote/Directory/CoolGameOnServer",
                        Tags = new List<string> { "survival", "lot of blood", "hardcore", "DONT PLAY AT NIGHT", "would play anyway" },
                        WasSuccessful = true
                    }
                }
            );
        }
    }
}
