using TheDevSpace.Domain.DomainExceptions;

namespace TheDevSpace.Domain.Entities;

public class User : Entity
{
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string Password { get; private set; }
    public DateTime RegistrationDateTime { get; private set; }
    public DateTime LastLogin { get; private set; }
    public Writer? Writer { get; private set; }
    public IReadOnlyList<ArticleStar> StarredArticles => _starredArticles;
    private List<ArticleStar> _starredArticles;

    public User(string email, string password, string name)
    {
        UserId = Guid.NewGuid();
        RegistrationDateTime = DateTime.Now;
        Email = email;
        Password = password;
        Name = name;
        _starredArticles = new List<ArticleStar>();

        Validate();
    }

    protected User() { }

    public override void Validate()
    {
        if (Name.IsNullOrEmpty())
            throw new DomainException("You must set a name!");

        if (Email.IsNullOrEmpty())
            throw new DomainException("You must set a email!");

        if (!Email.IsValidEmailAddress())
            throw new DomainException("You must set a valid email!");
    }

    public void ChangePassword(string password)
    {
        if (password.IsNullOrEmpty())
            throw new DomainException("Password is incorrect!");

        Password = password;
    }

    public void UpdateName(string name)
    {
        if (name.IsNullOrEmpty())
            throw new DomainException("You must set a name!");

        Name = name;
    }
}
