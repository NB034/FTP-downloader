using AutoMapper;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.UI.DataSources.DataTypes;

namespace FtpDownloader.UI.DataSources.Mappers
{
    public class LogicLayerMapper
    {
        private readonly Mapper _downloadToDtoMapper;
        private readonly Mapper _dtoToDownloadMapper;

        private readonly Mapper _entryToDtoMapper;
        private readonly Mapper _dtoToEntryMapper;

        public LogicLayerMapper()
        {
            _downloadToDtoMapper = InitializeDownloadToDto();
            _dtoToDownloadMapper = InitializeDtoToDownload();

            _entryToDtoMapper = InitializeEntryToDto();
            _dtoToEntryMapper = InitializeDtoToEntry();
        }



        public LogicLayerDownloadDto DownloadToDto(Download_VM viewModel) => _downloadToDtoMapper.Map<LogicLayerDownloadDto>(viewModel);
        public Download_VM DtoToDownload(LogicLayerDownloadDto dto) => _dtoToDownloadMapper.Map<Download_VM>(dto);

        public LogicLayerEntryDto EntryToDto(JournalEntry_VM viewModel) => _entryToDtoMapper.Map<LogicLayerEntryDto>(viewModel);
        public JournalEntry_VM DtoToEntry(LogicLayerEntryDto dto) => _dtoToEntryMapper.Map<JournalEntry_VM>(dto);



        private Mapper InitializeDownloadToDto()
        {
            throw new NotImplementedException();
        }

        private Mapper InitializeDtoToDownload()
        {
            throw new NotImplementedException();
        }

        private Mapper InitializeEntryToDto()
        {
            throw new NotImplementedException();
        }

        private Mapper InitializeDtoToEntry()
        {
            throw new NotImplementedException();
        }
    }
}
