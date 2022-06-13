namespace TheDevSpace.Application;

public class WriterDto
{
    public Guid WriterId { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }
    public string Role { get; set; }
    public UserDto User { get; set; }
    public Guid UserId { get; set; }
    public DateTime RegistrationDateTime { get; set; }
    public List<ArticleDto> Articles { get; set; }
}
