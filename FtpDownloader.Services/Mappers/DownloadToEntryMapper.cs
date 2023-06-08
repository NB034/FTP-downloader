using AutoMapper;
using FtpDownloader.Services.DataTypes;

namespace FtpDownloader.Services.Mappers
{
    public class DownloadToEntryMapper
    {
        private readonly Mapper _downloadToEntryMapper;

        public DownloadToEntryMapper()
        {
            _downloadToEntryMapper = InitializeDownloadToEntryMapper();
        }

        public Entry DownloadToEntry(Download model) => _downloadToEntryMapper.Map<Entry>(model);

        private Mapper InitializeDownloadToEntryMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Download, Entry>()
            .ForMember(nameof(Entry.RemotePath), opt => opt.MapFrom(d => d.From))
            .ForMember(nameof(Entry.LocalPath), opt => opt.MapFrom(d => Path.Combine(d.To, d.Name)))
            .ForMember(nameof(Entry.RemotePath), opt => opt.MapFrom(d => d.From))
            .ForMember(nameof(Entry.DownloadDate), opt => opt.MapFrom(d => d.DownloadDate))
            .ForMember(nameof(Entry.FileSize), opt => opt.MapFrom(d => d.Size))
            .ForMember(nameof(Entry.WasSuccessful), opt => opt.MapFrom(d => !d.Cancelling))
            .ForMember(nameof(Entry.Tags), opt => opt.MapFrom(d => d.Tags)));

            return new Mapper(config);
        }
    }
}
