using AutoMapper;
using FtpDownloader.DataAccess.Interfaces.DTO;
using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Mappers
{
    public class LogicLayerMapper
    {
        private readonly Mapper _dtoToEntryMapper;
        private readonly Mapper _entryToDtoMapper;

        private readonly Mapper _dtoToDownloadMapper;
        private readonly Mapper _downloadToDtoMapper;

        private readonly Mapper _dtoToInfoMapper;
        private readonly Mapper _infoToDtoMapper;

        public LogicLayerMapper()
        {
            _dtoToEntryMapper = InitializeDtoToEntry();
            _entryToDtoMapper = InitializeEntryToDto();

            _dtoToDownloadMapper = InitializeDtoToDownload();
            _downloadToDtoMapper = InitializeDownloadToDto();

            _dtoToInfoMapper = InitializeDtoToInfo();
            _infoToDtoMapper = InitializeInfoToDto();
        }

        public LogicLayerEntryDto EntryToDto(Entry entry) => _entryToDtoMapper.Map<LogicLayerEntryDto>(entry);
        public Entry DtoToEntry(LogicLayerEntryDto dto) => _dtoToEntryMapper.Map<Entry>(dto);

        public LogicLayerDownloadDto DownloadToDto(Download download) => _downloadToDtoMapper.Map<LogicLayerDownloadDto>(download);
        public Download DtoToDownload(LogicLayerDownloadDto dto) => _dtoToDownloadMapper.Map<Download>(dto);

        public LogicLayerInfoDto InfoToDto(Info info) => _infoToDtoMapper.Map<LogicLayerInfoDto>(info);
        public Info DtoToInfo(LogicLayerInfoDto dto) => _dtoToInfoMapper.Map<Info>(dto);




        private Mapper InitializeDtoToEntry()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LogicLayerEntryDto, Entry>());
            return new Mapper(config);
        }

        private Mapper InitializeEntryToDto()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entry, LogicLayerEntryDto>());
            return new Mapper(config);
        }



        private Mapper InitializeDtoToDownload()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LogicLayerDownloadDto, Download>());
            return new Mapper(config);
        }

        private Mapper InitializeDownloadToDto()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Download, LogicLayerDownloadDto>());
            return new Mapper(config);
        }



        private Mapper InitializeDtoToInfo()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LogicLayerInfoDto, Info>());
            return new Mapper(config);
        }

        private Mapper InitializeInfoToDto()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Info, LogicLayerInfoDto>());
            return new Mapper(config);
        }
    }
}
