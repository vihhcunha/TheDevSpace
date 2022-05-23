namespace TheDevSpace.Domain.Entities;

public class ArticleStar
{
    public Guid ArticleStarId { get; private set; }
    public Article Article { get; private set; }
    public Guid ArticleId { get; private set; }
    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime StarDateTime { get; private set; }

    public ArticleStar(Guid articleId, Guid userId)
    {
        ArticleStarId = Guid.NewGuid();
        ArticleId = articleId;
        UserId = userId;
        StarDateTime = DateTime.Now;
    }

    protected ArticleStar() { }
}
