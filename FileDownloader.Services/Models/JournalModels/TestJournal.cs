namespace FileDownloader.Services.Models.JournalModels
{
    internal class TestJournal : IJournal
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
        }

        public Task CreateEntry(JournalEntryModel entry)
        {
            _journalEntries.Add(entry);
            EntryCreated?.Invoke();
            return Task.CompletedTask;
        }

        public Task DeleteAllEntries()
        {
            _journalEntries.Clear();
            AllEntriesDeleted?.Invoke();
            return Task.CompletedTask;
        }

        public Task DeleteEntry(JournalEntryModel entry)
        {
            _journalEntries.Remove(entry);
            EntryDeleted?.Invoke();
            return Task.CompletedTask;
        }

        public Task<JournalEntryModel[]> GetEntries()
        {
            EntriesLoaded?.Invoke();
            return Task.FromResult(_journalEntries.ToArray());
        }
    }
}
