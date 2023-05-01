using AutoMapper;
using File_downloader.ViewModels;
using FileDownloader.Services.Models;

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

        public DownloadViewModel ModelToVm(DownloadModel model) => _modelToVmMapper.Map<DownloadViewModel>(model);
        public DownloadModel VmToModel(DownloadViewModel vm) => _vmToModelMapper.Map<DownloadModel>(vm);

        private Mapper InitializeVmToModelMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DownloadViewModel, DownloadModel>());

            return new Mapper(config);
        }

        private Mapper InitializeModelToVmMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DownloadViewModel, DownloadModel>());

            return new Mapper(config);
        }
    }
}
