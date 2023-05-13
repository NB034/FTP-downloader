using AutoMapper;
using File_downloader.ViewModels.DataViewModels;
using FileDownloader.Services.Models.DownloaderModels;

namespace File_downloader.Mappers
{
    internal class DownloadMapper
    {
        private readonly Mapper _vmToModelMapper;
        private readonly Mapper _modelToVmMapper;

        public DownloadMapper()
        {
            _vmToModelMapper = InitializeVmToModelMapper();
            _modelToVmMapper = InitializeModelToVmMapper();
        }

        public Download_VM ModelToVm(DownloadModel model) => _modelToVmMapper.Map<Download_VM>(model);
        public DownloadModel VmToModel(Download_VM vm) => _vmToModelMapper.Map<DownloadModel>(vm);

        private Mapper InitializeVmToModelMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Download_VM, DownloadModel>()
            .ForMember(nameof(DownloadModel.DownloadGuid), opt => opt.MapFrom(vm => vm.DownloadGuid)));

            return new Mapper(config);
        }

        private Mapper InitializeModelToVmMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DownloadModel, Download_VM>());

            return new Mapper(config);
        }
    }
}
