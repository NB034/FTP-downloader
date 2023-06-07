using AutoMapper;
using FtpDownloader.DataAccess.Interfaces.DTO;
using FtpDownloader.Services.Models.JournalModels;

namespace FtpDownloader.Services.Mappers
{
    public class JournalEntryToDtoMapper
    {
        private readonly Mapper _dtoToEntryMapper;
        private readonly Mapper _entryToDtoMapper;

        public JournalEntryToDtoMapper()
        {
            _dtoToEntryMapper = InitializeDtoToEntryMapper();
            _entryToDtoMapper = InitializeEntryToDtoMapper();
        }

        public JournalEntryEntityDto EntryToDto(JournalEntryModel model) => _entryToDtoMapper.Map<JournalEntryEntityDto>(model);
        public JournalEntryModel DtoToEntry(JournalEntryEntityDto entity) => _dtoToEntryMapper.Map<JournalEntryModel>(entity);

        private Mapper InitializeDtoToEntryMapper()
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntryEntityDto, JournalEntryModel>());

            return new Mapper(config);
        }

        private Mapper InitializeEntryToDtoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntryModel, JournalEntryEntityDto>());

            return new Mapper(config);
        }
    }
}
