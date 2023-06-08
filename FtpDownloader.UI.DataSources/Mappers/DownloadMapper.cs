using AutoMapper;
using FtpDownloader.Services.DataTypes;
using FtpDownloader.UI.DataSources.DataTypes;

namespace FtpDownloader.UI.DataSources.Mappers
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

        public Download_VM ModelToVm(Download model) => _modelToVmMapper.Map<Download_VM>(model);
        public Download VmToModel(Download_VM vm) => _vmToModelMapper.Map<Download>(vm);

        private Mapper InitializeVmToModelMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Download_VM, Download>()
            .ForMember(nameof(Download.DownloadGuid), opt => opt.MapFrom(vm => vm.DownloadGuid)));

            return new Mapper(config);
        }

        private Mapper InitializeModelToVmMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Download, Download_VM>());

            return new Mapper(config);
        }
    }
}
