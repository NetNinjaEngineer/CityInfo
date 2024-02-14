using AutoMapper;

namespace CityInfo.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityForCreationDto>().ReverseMap();
        CreateMap<City, CityForUpdateDto>().ReverseMap();
        CreateMap<PointOfInterest, PointOfInterestDto>();
        CreateMap<PointOfInterest, PointOfInterestForUpdateDto>().ReverseMap();
        CreateMap<CityForUpdateDto, City>();

    }
}
