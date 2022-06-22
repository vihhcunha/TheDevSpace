using TheDevSpace.Application;
using TheDevSpaceWebApp.ViewModels.Article;

namespace TheDevSpaceWebApp.ViewModels.Writer;

public class WriterViewModel
{
    public Guid WriterId { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
    public DateTime RegistrationDateTime { get; set; }

    public List<ArticleViewModel> Articles { get; set; }

    public static WriterViewModel WriterDtoToViewModel(WriterDto writerDto)
    {
        var writerViewModel = new WriterViewModel
        {
            Age = writerDto.Age,
            Description = writerDto.Description,
            Name = writerDto.User.Name,
            RegistrationDateTime = writerDto.RegistrationDateTime,
            Role = writerDto.Role,
            WriterId = writerDto.WriterId
        };

        if (writerDto.Articles != null && writerDto.Articles.Any())
            writerViewModel.Articles = ArticleViewModel.ArticlesToArticlesViewModel(writerDto.Articles);

        return writerViewModel;
    }
}
