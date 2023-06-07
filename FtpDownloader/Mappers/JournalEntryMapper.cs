using AutoMapper;
using File_downloader.Resources.ResourcesAccess;
using File_downloader.ViewModels.DataViewModels;
using FileDownloader.Services.Models.JournalModels;
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

        public JournalEntry_VM ModelToVm(JournalEntryModel model) => _modelToVmMapper.Map<JournalEntry_VM>(model);
        public JournalEntryModel VmToModel(JournalEntry_VM vm) => _vmToModelMapper.Map<JournalEntryModel>(vm);

        private Mapper InitializeVmToModelMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntry_VM, JournalEntryModel>()
            .ForMember(nameof(JournalEntryModel.LocalPath), opt => opt.MapFrom(vm => Path.Combine(vm.LocalPath, vm.FileName))));

            return new Mapper(config);
        }

        private Mapper InitializeModelToVmMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntryModel, JournalEntry_VM>()
            .ForMember(nameof(JournalEntry_VM.LocalPath), opt => opt.MapFrom(mod => Path.GetPathRoot(mod.LocalPath)))
            .ForMember(nameof(JournalEntry_VM.FileName), opt => opt.MapFrom(mod => Path.GetFileName(mod.LocalPath)))
            .ForMember(nameof(JournalEntry_VM.Result), opt => opt.MapFrom(mod => mod.WasSuccessful ? IconsManager.PositiveIcon : IconsManager.NegativeIcon)));

            return new Mapper(config);
        }
    }
}
