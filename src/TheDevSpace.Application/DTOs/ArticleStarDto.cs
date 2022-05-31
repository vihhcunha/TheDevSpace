namespace TheDevSpace.Application;

public class ArticleStarDto
{
    public Guid ArticleStarId { get; set; }
    public ArticleDto Article { get; set; }
    public Guid ArticleId { get; set; }
    public UserDto User { get; set; }
    public Guid UserId { get; set; }
    public DateTime StarDateTime { get; set; }
}
