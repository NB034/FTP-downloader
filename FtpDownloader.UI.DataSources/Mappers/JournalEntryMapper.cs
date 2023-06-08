using AutoMapper;
using System.IO;
using FtpDownloader.Services.DataTypes;
using FtpDownloader.UI.DataSources.Accessories;
using FtpDownloader.UI.DataSources.DataTypes;

namespace FtpDownloader.UI.DataSources.Mappers
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

        public JournalEntry_VM ModelToVm(Entry model) => _modelToVmMapper.Map<JournalEntry_VM>(model);
        public Entry VmToModel(JournalEntry_VM vm) => _vmToModelMapper.Map<Entry>(vm);

        private Mapper InitializeVmToModelMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntry_VM, Entry>()
            .ForMember(nameof(Entry.LocalPath), opt => opt.MapFrom(vm => Path.Combine(vm.LocalPath, vm.FileName))));

            return new Mapper(config);
        }

        private Mapper InitializeModelToVmMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entry, JournalEntry_VM>()
            .ForMember(nameof(JournalEntry_VM.LocalPath), opt => opt.MapFrom(mod => Path.GetPathRoot(mod.LocalPath)))
            .ForMember(nameof(JournalEntry_VM.FileName), opt => opt.MapFrom(mod => Path.GetFileName(mod.LocalPath)))
            .ForMember(nameof(JournalEntry_VM.Result), opt => opt.MapFrom(mod => mod.WasSuccessful ? NotificationTypesEnum.Positive : NotificationTypesEnum.Negative)));

            return new Mapper(config);
        }
    }
}
