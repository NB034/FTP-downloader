using AutoMapper;
using FtpDownloader.Services.Interfaces.DTO;
using System.IO;

namespace FtpDownloader.UI.DataSources.Mappers
{
    public class DownloadDtoToEntryDtoMapper
    {
        private readonly Mapper _downloadToEntryMapper;

        public DownloadDtoToEntryDtoMapper()
        {
            _downloadToEntryMapper = InitializeDownloadToEntry();
        }



        public LogicLayerEntryDto DownloadToEntry(LogicLayerDownloadDto downloadDto) => _downloadToEntryMapper.Map<LogicLayerEntryDto>(downloadDto);



        private Mapper InitializeDownloadToEntry()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LogicLayerDownloadDto, LogicLayerEntryDto>()
            .ForMember(nameof(LogicLayerEntryDto.RemotePath), opt => opt.MapFrom(d => d.From))
            .ForMember(nameof(LogicLayerEntryDto.LocalPath), opt => opt.MapFrom(d => Path.Combine(d.To, d.Name)))
            .ForMember(nameof(LogicLayerEntryDto.RemotePath), opt => opt.MapFrom(d => d.From))
            .ForMember(nameof(LogicLayerEntryDto.FileSize), opt => opt.MapFrom(d => d.Size))
            .ForMember(nameof(LogicLayerEntryDto.WasSuccessful), opt => opt.MapFrom(d => !d.Cancelling)));

            return new Mapper(config);
        }
    }
}
