﻿using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.TestModels
{
    public class Test_Journal : IJournal
    {
        private List<Entry> _journalEntries;
        private readonly LogicLayerMapper _mapper;

        public Test_Journal(LogicLayerMapper mapper)
        {
            _journalEntries = new List<Entry>();
            _mapper = mapper;
            Seed();
        }

        public event EventHandler AllEntriesDeleted;
        public event EventHandler EntriesLoaded;
        public event EventHandler EntryCreated;
        public event EventHandler EntryDeleted;
        public event EventHandler<ExceptionThrownedEventArgs> ExceptionThrowned;




        public async Task DeleteAllEntries()
        {
            await Task.Delay(2000);
            _journalEntries.Clear();
            AllEntriesDeleted?.Invoke(this, EventArgs.Empty);
        }

        public async Task CreateEntry(LogicLayerEntryDto dto)
        {
            await Task.Delay(2000);
            _journalEntries.Add(_mapper.DtoToEntry(dto));
            EntryCreated?.Invoke(this, EventArgs.Empty);
        }

        public async Task DeleteEntry(LogicLayerEntryDto dto)
        {
            await Task.Delay(2000);
            _journalEntries.Remove(_mapper.DtoToEntry(dto));
            EntryDeleted?.Invoke(this, EventArgs.Empty);
        }

        public async Task<LogicLayerEntryDto[]> GetEntries()
        {
            await Task.Delay(2000);
            var dtos = new List<LogicLayerEntryDto>();
            foreach (var entry in _journalEntries)
            {
                dtos.Add(_mapper.EntryToDto(entry));
            }
            EntriesLoaded?.Invoke(this, EventArgs.Empty);
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
                        LocalPath = "SomeDirectory/SomeFile",
                        RemotePath = "SomeRemoteDirectory/SomeFile",
                        Tags = new List<string> { "test", "test", "test", "test", "test" },
                        WasSuccessful = true
                    },
                    new Entry
                    {
                        DownloadDate = DateTime.Now,
                        FileSize = 10_000,
                        Id = 2,
                        LocalPath = "SomeDirectory/SomeFile",
                        RemotePath = "SomeRemoteDirectory/SomeFile",
                        Tags = new List<string> { "test", "test", "test", "test", "test" },
                        WasSuccessful = false
                    },
                    new Entry
                    {
                        DownloadDate = DateTime.Now,
                        FileSize = 10_000,
                        Id = 3,
                        LocalPath = "SomeDirectory/SomeFile",
                        RemotePath = "SomeRemoteDirectory/SomeFile",
                        Tags = new List<string> { "test", "test", "test", "test", "test" },
                        WasSuccessful = true
                    }
                }
            );
        }
    }
}
