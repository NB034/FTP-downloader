using AutoMapper;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.UI.DataSources.Accessories;
using FtpDownloader.UI.DataSources.DataTypes;
using System.IO;

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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Download_VM, LogicLayerDownloadDto>());
            return new Mapper(config);
        }

        private Mapper InitializeDtoToDownload()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LogicLayerDownloadDto, Download_VM>());
            return new Mapper(config);
        }

        private Mapper InitializeEntryToDto()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntry_VM, LogicLayerEntryDto>()
            .ForMember(nameof(LogicLayerEntryDto.WasSuccessful), opt => opt.MapFrom(vm => vm.Result == NotificationTypesEnum.Positive))
            .ForMember(nameof(LogicLayerEntryDto.LocalPath), opt => opt.MapFrom(vm => Path.Combine(vm.LocalPath, vm.FileName))));
            return new Mapper(config);
        }

        private Mapper InitializeDtoToEntry()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LogicLayerEntryDto, JournalEntry_VM>()
            .ForMember(nameof(JournalEntry_VM.Result), opt => opt.MapFrom(dto => dto.WasSuccessful ? NotificationTypesEnum.Positive : NotificationTypesEnum.Negative))
            .ForMember(nameof(JournalEntry_VM.FileName), opt => opt.MapFrom(dto => Path.GetFileName(dto.LocalPath)))
            .ForMember(nameof(JournalEntry_VM.LocalPath), opt => opt.MapFrom(dto => Path.GetDirectoryName(dto.LocalPath))));
            return new Mapper(config);
        }
    }
}
