using TheDevSpace.Domain.DomainExceptions;

namespace TheDevSpace.Domain.Entities;

public class User : Entity
{
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime RegistrationDateTime { get; private set; }
    public DateTime LastLogin { get; private set; }
    public Guid? WriterId { get; private set; }
    public Writer? Writer { get; private set; }
    public List<ArticleStar> StarredArticles { get; private set; }

    public User(string email, string password)
    {
        Email = email;
        Password = password;

        Validate();
    }

    public User(string email, string password, Guid writerId)
    {
        UserId = Guid.NewGuid();
        RegistrationDateTime = DateTime.Now;
        Email = email;
        Password = password;
        WriterId = writerId;

        Validate();
    }

    public override void Validate()
    {
        if (Email.IsNullOrEmpty())
            throw new DomainException("You must set a email!");

        if (!Email.IsValidEmailAddress())
            throw new DomainException("You must set a valid email!");
    }
}
