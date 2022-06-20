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
}
