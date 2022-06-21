using TheDevSpace.Application;

namespace TheDevSpaceWebApp.ViewModels.Article;

public class ArticleViewModel
{
    public Guid ArticleId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    public Guid WriterId { get; set; }
    public DateTime Launch { get; set; }
    public int StarsCount { get; set; }
    public string WriterName { get; set; }
    public bool StarredByCurrentUser { get; set; }

    public static List<ArticleViewModel> ArticlesToArticlesViewModel(List<ArticleDto> articles)
    {
        return articles.Select(a => new ArticleViewModel
        {
            ArticleId = a.ArticleId,
            Content = a.Content,
            Description = a.Description,
            Launch = a.Launch,
            StarsCount = a.Stars.Count,
            Title = a.Title,
            WriterId = a.WriterId,
            WriterName = a.Writer.User.Name
        }).ToList();
    }

    public static ArticleViewModel ArticleToArticleViewModel(ArticleDto article)
    {
        return new ArticleViewModel
        {
            ArticleId = article.ArticleId,
            Content = article.Content,
            Description = article.Description,
            Launch = article.Launch,
            StarsCount = article.Stars.Count,
            Title = article.Title,
            WriterId = article.WriterId,
            WriterName = article.Writer.User.Name
        };
    }
}
