using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public interface IArticleRepository : IRepository<Article>
{
    Task<List<Article>> GetAllArticles();
    Task AddArticle(Article article);
    Task<List<Article>> GetArticlesByWriter(Guid writerId);
    Task<List<Article>> GetArticlesByWriter(Guid writerId, string search);
    Task<List<Article>> SearchArticlesByTitle(string search);
    Task AddStar(ArticleStar articleStar);
    Task<Article> GetArticleWithStars(Guid articleId);
    Task<ArticleStar> GetArticleStar(Guid articleStarId);
    Task DeleteArticle(Guid articleId);
    Task DeleteStar(Guid starId);
}
