using AutoMapper;
using DemoAPIAzure.DTOs;
using DemoAPIAzure.Entities;

namespace DemoAPIAzure.Services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category, CategoryDTO>()
                .ForMember(dto => dto.ToDos, ent => ent.MapFrom(prop => 
                    prop.ToDos.OrderByDescending(td => td.Priority)
                        .OrderBy(td => td.Title)));
            CreateMap<Category, ToDoCategoryDTO>();
            CreateMap<ToDo, ToDoDTO>()
                .ForMember(dto => dto.Category, ent => ent.MapFrom(prop => prop.Category));
            CreateMap<ToDo, CategoryToDoDTO>();
        }
    }
}
