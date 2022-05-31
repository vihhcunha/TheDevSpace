using AutoMapper;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Application.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Article, ArticleDto>();
        CreateMap<ArticleStar, ArticleStarDto>();
        CreateMap<User, UserDto>();
        CreateMap<Writer, WriterDto>();
    }
}
