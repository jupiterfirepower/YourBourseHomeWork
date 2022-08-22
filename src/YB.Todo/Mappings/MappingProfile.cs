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
        }
    }
}
