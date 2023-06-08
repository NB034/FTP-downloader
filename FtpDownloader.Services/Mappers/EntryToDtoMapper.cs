using AutoMapper;
using FtpDownloader.DataAccess.Interfaces.DTO;
using FtpDownloader.Services.Models.JournalModels;

namespace FtpDownloader.Services.Mappers
{
    public class EntryToDtoMapper
    {
        private readonly Mapper _dtoToEntryMapper;
        private readonly Mapper _entryToDtoMapper;

        public EntryToDtoMapper()
        {
            _dtoToEntryMapper = InitializeDtoToEntryMapper();
            _entryToDtoMapper = InitializeEntryToDtoMapper();
        }

        public EntryEntityDto EntryToDto(JournalEntryModel model) => _entryToDtoMapper.Map<EntryEntityDto>(model);
        public JournalEntryModel DtoToEntry(EntryEntityDto entity) => _dtoToEntryMapper.Map<JournalEntryModel>(entity);

        private Mapper InitializeDtoToEntryMapper()
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<EntryEntityDto, JournalEntryModel>());

            return new Mapper(config);
        }

        private Mapper InitializeEntryToDtoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntryModel, EntryEntityDto>());

            return new Mapper(config);
        }
    }
}
