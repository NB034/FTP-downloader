using AutoMapper;
using File_downloader.ViewModels;
using FileDownloader.Services.Models;
using System.IO;

namespace File_downloader.Mappers
{
    internal class JournalEntryMapper
    {
        private readonly Mapper _vmToModelMapper;
        private readonly Mapper _modelToVmMapper;

        public JournalEntryMapper()
        {
            _vmToModelMapper = InitializeVmToModelMapper();
            _modelToVmMapper = InitializeModelToVmMapper();
        }

        public JournalEntryViewModel ModelToVm(JournalEntryModel model) => _modelToVmMapper.Map<JournalEntryViewModel>(model);
        public JournalEntryModel VmToModel(JournalEntryViewModel vm) => _vmToModelMapper.Map<JournalEntryModel>(vm);

        private Mapper InitializeVmToModelMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntryViewModel, JournalEntryModel>()
            .ForMember(nameof(JournalEntryModel.LocalPath), opt => opt.MapFrom(vm => Path.Combine(vm.LocalPath, vm.FileName))));

            return new Mapper(config);
        }

        private Mapper InitializeModelToVmMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntryModel, JournalEntryViewModel>()
            .ForMember(nameof(JournalEntryViewModel.LocalPath), opt => opt.MapFrom(mod => Path.GetPathRoot(mod.LocalPath)))
            .ForMember(nameof(JournalEntryViewModel.FileName), opt => opt.MapFrom(mod => Path.GetFileName(mod.LocalPath))));

            return new Mapper(config);
        }
    }
}
