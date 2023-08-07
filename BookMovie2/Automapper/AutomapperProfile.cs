using AutoMapper;

namespace BookMovie2.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Data.Movie, Models.Movie>();//how
            CreateMap<Data.Movie, Models.Movie>().ReverseMap();

            CreateMap<Data.Audi, Models.Audi>();
            CreateMap<Data.Audi, Models.Audi>().ReverseMap();

            CreateMap<Data.Theatre, Models.Theatre>();
            CreateMap<Data.Theatre, Models.Theatre>().ReverseMap();

            CreateMap<Data.User, Models.User>();
            CreateMap<Data.User, Models.User>().ReverseMap();
           

        }
    }
}
