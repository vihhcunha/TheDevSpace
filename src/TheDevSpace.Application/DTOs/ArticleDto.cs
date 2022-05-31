namespace TheDevSpace.Application;

public class ArticleDto
{
    public Guid ArticleId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public WriterDto Writer { get; set; }
    public Guid WriterId { get; set; }
    public DateTime Launch { get; set; }
    public List<ArticleStarDto> Stars { get; set; }
}
