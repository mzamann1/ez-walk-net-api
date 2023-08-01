using AutoMapper;
using EZWalk.API.DTOs.Difficulty;
using EZWalk.API.DTOs.Region;
using EZWalk.API.DTOs.Walk;
using EZWalk.API.Models;

namespace EZWalk.API.Mapping
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionRequestDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDTO>().ReverseMap();


            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateDifficultyRequestDto, Walk>().ReverseMap();


            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<Difficulty, AddDifficultyRequestDto>().ReverseMap();
            CreateMap<Difficulty, UpdateDifficultyRequestDto>().ReverseMap();
        }
    }
}
