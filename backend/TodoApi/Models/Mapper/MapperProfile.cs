using AutoMapper;
using TodoApi.Models.Dto;

namespace TodoApi.Models.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<TaskCreateRequest, TodoTask>();
    }
}