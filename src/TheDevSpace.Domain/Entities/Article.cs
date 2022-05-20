using TheDevSpace.Domain.DomainExceptions;

namespace TheDevSpace.Domain.Entities;

public class Article : Entity
{
    public Guid ArticleId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public Writer Writer { get; private set; }
    public Guid WriterId { get; private set; }
    public DateTime Launch { get; private set; }
    public IReadOnlyList<ArticleStar> Stars => _stars;
    private List<ArticleStar> _stars;

    public Article(string title, string content, Guid writerId)
    {
        Title = title;
        Content = content;
        WriterId = writerId;
        ArticleId = Guid.NewGuid();
        Launch = DateTime.Now;
        _stars = new List<ArticleStar>();

        Validate();
    }

    public override void Validate()
    {
        if (Title.IsNullOrEmpty())
            throw new DomainException("You must set a title!");

        if (Content.IsNullOrEmpty())
            throw new DomainException("You must set a content!");
    }

    public void GiveStar(Guid userId)
    {
        if (Stars == null) 
            _stars = new List<ArticleStar>();

        _stars.Add(new ArticleStar(ArticleId, userId));
    }
}
