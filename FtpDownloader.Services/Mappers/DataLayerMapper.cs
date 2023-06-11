using AutoMapper;
using FtpDownloader.DataAccess.Interfaces.DTO;
using FtpDownloader.Services.DataTypes;

namespace FtpDownloader.Services.Mappers
{
    public class DataLayerMapper
    {
        private readonly Mapper _dtoToEntryMapper;
        private readonly Mapper _entryToDtoMapper;

        public DataLayerMapper()
        {
            _dtoToEntryMapper = InitializeDtoToEntryMapper();
            _entryToDtoMapper = InitializeEntryToDtoMapper();
        }

        public DataLayerEntryDto EntryToDto(Entry model) => _entryToDtoMapper.Map<DataLayerEntryDto>(model);
        public Entry DtoToEntry(DataLayerEntryDto entity) => _dtoToEntryMapper.Map<Entry>(entity);



        private Mapper InitializeDtoToEntryMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DataLayerEntryDto, Entry>());
            return new Mapper(config);
        }

        private Mapper InitializeEntryToDtoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entry, DataLayerEntryDto>());
            return new Mapper(config);
        }
    }
}
