using AutoMapper;
using System;
using YB.Todo.DtoModels;
using YB.Todo.Entities;

namespace YB.Todo.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoEntity, ToDoItem>()
                .ReverseMap();

            CreateMap<AddToDoItem, ToDoEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(c => 0))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(c => c.IsComplete))
                .ForMember(dest => dest.CreatedOnUtc, opt => opt.MapFrom(c => DateTime.UtcNow));

            CreateMap<UpdateToDoItem, ToDoEntity>()
                .ForMember(dest => dest.CreatedOnUtc, opt => opt.MapFrom(c => DateTime.UtcNow))
                .ForMember(dest => dest.LastModifiedOnUtc, opt => opt.MapFrom(c => DateTime.UtcNow));
        }
    }
}
