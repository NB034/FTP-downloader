using AutoMapper;
using FtpDownloader.DataAccess.Entities;
using FtpDownloader.DataAccess.Interfaces.DTO;

namespace FtpDownloader.DataAccess.Mappers
{
    public class DataLayerMapper
    {
        private readonly Mapper _dtoToEntityMapper;
        private readonly Mapper _entityToDtoMapper;

        public DataLayerMapper()
        {
            _dtoToEntityMapper = InitializeDtoToEntityMapper();
            _entityToDtoMapper = InitializeEntityToDtoMapper();
        }

        public DataLayerEntryDto EntityToDto(EntryEntity entity) => _entityToDtoMapper.Map<DataLayerEntryDto>(entity);
        public EntryEntity DtoToEntity(DataLayerEntryDto dto) => _dtoToEntityMapper.Map<EntryEntity>(dto);

        private Mapper InitializeDtoToEntityMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DataLayerEntryDto, EntryEntity>()
            .ForMember(nameof(EntryEntity.TagEntities), opt => opt.Ignore()));

            return new Mapper(config);
        }

        private Mapper InitializeEntityToDtoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EntryEntity, DataLayerEntryDto>()
            .ForMember(nameof(DataLayerEntryDto.Tags), opt => opt.MapFrom(e => e.TagEntities.Select(t => t.Name))));

            return new Mapper(config);
        }
    }
}
