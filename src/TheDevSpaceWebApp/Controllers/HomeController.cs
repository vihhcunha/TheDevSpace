using Microsoft.AspNetCore.Mvc;
using TheDevSpace.Application;
using TheDevSpaceWebApp.ViewModels.Article;

namespace TheDevSpaceWebApp.Controllers;

public class HomeController : Controller
{
    private readonly IArticleService _articleService;

    public HomeController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] string? search)
    {
        throw new Exception("aaaaa");

        List<ArticleDto> articles;
        if (search == null)
        {
            articles = await _articleService.GetAllArticles();
        }
        else
        {
            articles = await _articleService.SearchArticlesByTitle(search);
        }

        var articlesViewModel = ArticleViewModel.ArticlesToArticlesViewModel(articles);
        return View(articlesViewModel);
    }
}
