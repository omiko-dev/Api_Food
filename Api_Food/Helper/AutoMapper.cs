using Api_Food.Models;
using AutoMapper;
using Food_Users_Api.Models;

namespace Api_Food.Helper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {

            CreateMap<RegisterDto, User>();
            CreateMap<FoodDto, Food>();
        }
    }
}
