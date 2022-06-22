using System.ComponentModel.DataAnnotations;

namespace TheDevSpaceWebApp.ViewModels.Article;

public class CreateEditArticleViewModel
{
    [Required(ErrorMessage = "You must set a title for this article!")]
    public string Title { get; set; }

    [Required(ErrorMessage = "You must set a content for this article!")]
    public string Content { get; set; }

    [Required(ErrorMessage = "You must set a description for this article!")]
    public string Description { get; set; }
    public Guid? WriterId { get; set; }
    public Guid? ArticleId { get; set; }
}
