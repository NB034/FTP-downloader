using AutoMapper;
using FtpDownloader.DataAccess.Entities;
using FtpDownloader.DataAccess.Interfaces.DTO;

namespace FtpDownloader.DataAccess.Mappers
{
    public class EntityToDtoMapper
    {
        private readonly Mapper _dtoToEntityMapper;
        private readonly Mapper _entityToDtoMapper;

        public EntityToDtoMapper()
        {
            _dtoToEntityMapper = InitializeDtoToEntityMapper();
            _entityToDtoMapper = InitializeEntityToDtoMapper();
        }

        public EntryEntityDto EntityToDto(EntryEntity entity) => _entityToDtoMapper.Map<EntryEntityDto>(entity);
        public EntryEntity DtoToEntity(EntryEntityDto dto) => _dtoToEntityMapper.Map<EntryEntity>(dto);

        private Mapper InitializeDtoToEntityMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EntryEntityDto, EntryEntity>()
            .ForMember(nameof(EntryEntity.TagEntities), opt => opt.Ignore()));

            return new Mapper(config);
        }

        private Mapper InitializeEntityToDtoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EntryEntity, EntryEntityDto>()
            .ForMember(nameof(EntryEntityDto.Tags), opt => opt.MapFrom(e => e.TagEntities.Select(t => t.Name))));

            return new Mapper(config);
        }
    }
}
