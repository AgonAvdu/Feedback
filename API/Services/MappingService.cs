using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateSubjectDto, Subject>();
            CreateMap<UpdateSubjectDto, Subject>();
            CreateMap<CreateCityDto, City>();
            CreateMap<UpdateCityDto, City>();

        }
    }
}
