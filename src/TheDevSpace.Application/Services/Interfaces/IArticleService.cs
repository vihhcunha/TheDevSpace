namespace TheDevSpace.Application;

public interface IArticleService
{
    Task<List<ArticleDto>> GetAllArticles();
    Task AddArticle(ArticleDto articleDto);
    Task EditArticle(ArticleDto articleDto);
    Task<List<ArticleDto>> GetArticlesByWriter(Guid writerId);
    Task<List<ArticleDto>> GetArticlesByWriter(Guid writerId, string search);
    Task<List<ArticleDto>> SearchArticlesByTitle(string search);
    Task AddStar(Guid userId, Guid articleId);
    Task<ArticleDto> GetArticleWithStars(Guid articleId);
    Task<ArticleStarDto> GetArticleStar(Guid articleStarId);
    Task DeleteArticle(Guid articleId);
    Task DeleteStar(Guid userId, Guid articleId);
}
