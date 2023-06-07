using AutoMapper;
using FileDownloader.DataAccess.Contexts;
using FileDownloader.DataAccess.Entities;
using FileDownloader.Services.Models.JournalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader.Services.Mappers
{
    internal class JournalEntryMapper
    {
        private readonly FileDownloaderDbContext _context;
        private readonly Mapper _entityToModelMapper;
        private readonly Mapper _modelToEntityMapper;

        public JournalEntryMapper(FileDownloaderDbContext context)
        {
            _context = context;
            _entityToModelMapper = InitializeEntityToModelMapper();
            _modelToEntityMapper = InitializeModelToEntityMapper();
        }

        public JournalEntryEntity ModelToEntity(JournalEntryModel model) => _modelToEntityMapper.Map<JournalEntryEntity>(model);
        public JournalEntryModel EntityToModel(JournalEntryEntity entity) => _entityToModelMapper.Map<JournalEntryModel>(entity);

        private Mapper InitializeEntityToModelMapper()
        {
            var tagsConverter = new Func<ICollection<TagEntity>, List<string>>(col =>
            {
                var tags = new List<string>();
                foreach (var tag in col)
                {
                    tags.Add(tag.Name);
                }
                return tags;
            });

            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntryEntity, JournalEntryModel>()
            .ForMember(nameof(JournalEntryModel.Tags), opt => opt.MapFrom(ent => tagsConverter(ent.TagEntities))));

            return new Mapper(config);
        }

        private Mapper InitializeModelToEntityMapper()
        {
            var tagsConverter = new Func<List<string>, ICollection<TagEntity>>(col =>
            {
                var tags = new List<TagEntity>();
                foreach (var tag in col)
                {
                    tags.Add(CreateTagIfNotExist(tag));
                }
                return tags.ToArray();
            });

            var config = new MapperConfiguration(cfg => cfg.CreateMap<JournalEntryModel, JournalEntryEntity>()
            .ForMember(nameof(JournalEntryEntity.TagEntities), opt => opt.MapFrom(mod => tagsConverter(mod.Tags))));

            return new Mapper(config);
        }

        private TagEntity CreateTagIfNotExist(string name)
        {
            var tag = _context.TagEntities.Where(t => t.Name == name).FirstOrDefault();
            if (tag == null)
            {
                tag = _context.TagEntities.Add(new TagEntity { Name = name }).Entity;
            }
            return tag;
        }
    }
}
