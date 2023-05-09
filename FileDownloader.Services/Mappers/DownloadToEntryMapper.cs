using AutoMapper;
using FileDownloader.Services.Models.DownloaderModels;
using FileDownloader.Services.Models.JournalModels;

namespace FileDownloader.Services.Mappers
{
    public class DownloadToEntryMapper
    {
        private readonly Mapper _downloadToEntryMapper;

        public DownloadToEntryMapper()
        {
            _downloadToEntryMapper = InitializeDownloadToEntryMapper();
        }

        public JournalEntryModel DownloadToEntry(DownloadModel model) => _downloadToEntryMapper.Map<JournalEntryModel>(model);

        private Mapper InitializeDownloadToEntryMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DownloadModel, JournalEntryModel>()
            .ForMember(nameof(JournalEntryModel.RemotePath), opt => opt.MapFrom(d => d.From))
            .ForMember(nameof(JournalEntryModel.LocalPath), opt => opt.MapFrom(d => Path.Combine(d.To, d.Name)))
            .ForMember(nameof(JournalEntryModel.RemotePath), opt => opt.MapFrom(d => d.From))
            .ForMember(nameof(JournalEntryModel.DownloadDate), opt => opt.MapFrom(d => d.DownloadDate))
            .ForMember(nameof(JournalEntryModel.FileSize), opt => opt.MapFrom(d => d.Size))
            .ForMember(nameof(JournalEntryModel.WasSuccessful), opt => opt.MapFrom(d => d.Cancelling))
            .ForMember(nameof(JournalEntryModel.Tags), opt => opt.MapFrom(d => d.Tags)));

            return new Mapper(config);
        }
    }
}
