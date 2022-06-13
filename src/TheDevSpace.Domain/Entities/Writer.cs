using TheDevSpace.Domain.DomainExceptions;

namespace TheDevSpace.Domain.Entities;

public class Writer : Entity
{
    public Guid WriterId { get; private set; }
    public int Age { get; private set; }
    public string Description { get; private set; }
    public string Role { get; private set; }
    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime RegistrationDateTime { get; private set; }
    public IReadOnlyList<Article> Articles => _articles;
    private List<Article> _articles;

    public Writer(int age, string description, string role, Guid userId)
    {
        WriterId = Guid.NewGuid();
        RegistrationDateTime = DateTime.Now;
        Age = age;
        Description = description;
        Role = role;
        UserId = userId;
        _articles = new List<Article>();

        Validate();
    }

    protected Writer() { }

    public override void Validate()
    {
        if (Description.IsNullOrEmpty())
            throw new DomainException("You must set a description!");

        if (Role.IsNullOrEmpty())
            throw new DomainException("You must set a role!");
    }
}
