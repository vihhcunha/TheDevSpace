using Microsoft.EntityFrameworkCore;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public class ArticleRepository : Repository<Article>, IArticleRepository 
{
    public ArticleRepository(TheDevSpaceContext context) : base(context) { }

    public async Task AddArticle(Article article)
    {
        await _context.Articles.AddAsync(article);
    }

    public async Task AddStar(ArticleStar articleStar)
    {
        await _context.ArticleStars.AddAsync(articleStar);
    }

    public async Task DeleteArticle(Guid articleId)
    {
        var article = await GetArticleWithStars(articleId);
        _context.Articles.Remove(article);
        _context.ArticleStars.RemoveRange(article.Stars);
    }

    public async Task DeleteStar(Guid starId)
    {
        var articleStar = await GetArticleStar(starId);
        _context.ArticleStars.Remove(articleStar);
    }

    public async Task<List<Article>> GetAllArticles()
    {
        return await _context.Articles
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<List<Article>> GetArticlesByWriter(Guid writerId)
    {
        return await _context.Articles.Where(a => a.WriterId == writerId).ToListAsync();
    }

    public async Task<ArticleStar> GetArticleStar(Guid articleStarId)
    {
        return await _context.ArticleStars.FindAsync(articleStarId);
    }

    public async Task<Article> GetArticleWithStars(Guid articleId)
    {
        return await _context.Articles
            .Include(a => a.Stars)
            .FirstOrDefaultAsync(a => a.ArticleId == articleId);
    }

    public async Task<List<Article>> SearchArticlesByTitle(string search)
    {
        return await _context.Articles
            .Where(a => a.Title.Contains(search))
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }
}
