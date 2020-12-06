using AutoMapper;
using Route.Models;
using Route.Models.Data;

namespace Route.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RouteViewModel, RouteInfo>();
            CreateMap<RouteInfo, RouteViewModel>();
        }
    }
}