namespace TheDevSpace.Application;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public DateTime RegistrationDateTime { get; set; }
    public DateTime LastLogin { get; set; }
    public Guid? WriterId { get; set; }
    public WriterDto? Writer { get; set; }
    public List<ArticleStarDto> StarredArticles { get; set; }
}
