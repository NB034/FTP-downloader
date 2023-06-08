
namespace FtpDownloader.Services.Models.JournalModels
{
    public class TestJournal : IJournal
    {
        private List<JournalEntryModel> _journalEntries;

        public event Action AllEntriesDeleted;
        public event Action EntriesLoaded;
        public event Action EntryCreated;
        public event Action EntryDeleted;
        public event Action<Exception> ExceptionThrowned;

        public TestJournal()
        {
            _journalEntries = new List<JournalEntryModel>();
            //{
            //    new JournalEntryModel
            //    {
            //         DownloadDate= DateTime.Now,
            //         FileSize= 10_000,
            //         Id = 1,
            //         LocalPath = "Some/Directory/NiceGame",
            //         RemotePath = "Some/Remote/Directory/CoolGameOnServer",
            //         Tags = new List<string>{"anime", "girls", "18+", "UWU", "lol"},
            //         WasSuccessful = true
            //    },
            //    new JournalEntryModel
            //    {
            //         DownloadDate= DateTime.Now,
            //         FileSize= 10_000,
            //         Id = 1,
            //         LocalPath = "Some/Directory/CoolGame",
            //         RemotePath = "Some/Remote/Directory/CoolGameOnServer",
            //         Tags = new List<string>{"shooter", "sci-fi", "18+", "bla", "random tag idk"},
            //         WasSuccessful = false
            //    },
            //    new JournalEntryModel
            //    {
            //         DownloadDate= DateTime.Now,
            //         FileSize= 10_000,
            //         Id = 1,
            //         LocalPath = "Some/Directory/ScaryGame",
            //         RemotePath = "Some/Remote/Directory/CoolGameOnServer",
            //         Tags = new List<string>{"survival", "lot of blood", "hardcore", "DONT PLAY AT NIGHT", "would play anyway"},
            //         WasSuccessful = true
            //    }
            //};
        }

        public async Task CreateEntry(JournalEntryModel entry)
        {
            await Task.Delay(2000);
            _journalEntries.Add(entry);
            EntryCreated?.Invoke();
        }

        public async Task DeleteAllEntries()
        {
            await Task.Delay(2000);
            _journalEntries.Clear();
            AllEntriesDeleted?.Invoke();
        }

        public async Task DeleteEntry(JournalEntryModel entry)
        {
            await Task.Delay(2000);
            _journalEntries.Remove(entry);
            EntryDeleted?.Invoke();
        }

        public async Task<JournalEntryModel[]> GetEntries()
        {
            await Task.Delay(2000);
            EntriesLoaded?.Invoke();
            return _journalEntries.ToArray();
        }
    }
}
