namespace TheDevSpace.Application;

public interface IArticleService
{
    Task<List<ArticleDto>> GetAllArticles();
    Task AddArticle(ArticleDto articleDto);
    Task<List<ArticleDto>> GetArticlesByWriter(Guid writerId);
    Task<List<ArticleDto>> SearchArticlesByTitle(string search);
    Task AddStar(ArticleStarDto articleStarDto);
    Task<ArticleDto> GetArticleWithStars(Guid articleId);
    Task<ArticleStarDto> GetArticleStar(Guid articleStarId);
    Task DeleteArticle(Guid articleId);
    Task DeleteStar(Guid starId);
}
