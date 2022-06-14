using AutoMapper;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Application.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Article, ArticleDto>();
        CreateMap<ArticleStar, ArticleStarDto>();
        CreateMap<User, UserDto>()
            .ForMember(nameof(User.Password), opt => opt.Ignore());
        CreateMap<Writer, WriterDto>();
    }
}
