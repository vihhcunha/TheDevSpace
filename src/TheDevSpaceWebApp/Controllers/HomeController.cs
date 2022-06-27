using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using TheDevSpace.Application;
using TheDevSpaceWebApp.ViewModels.Article;

namespace TheDevSpaceWebApp.Controllers;

public class HomeController : Controller
{
    private readonly IArticleService _articleService;
    private readonly IDistributedCache _cache;

    public HomeController(IArticleService articleService, IDistributedCache cache)
    {
        _articleService = articleService;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] string? search)
    {
        var cachedArticles = await _cache.GetStringAsync("Articles");
        if (!cachedArticles.IsNullOrEmpty())
        {
            return View(JsonSerializer.Deserialize<List<ArticleViewModel>>(cachedArticles));
        }

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
        var articlesViewModelJson = JsonSerializer.Serialize(articlesViewModel);
        await _cache.SetStringAsync("Articles", articlesViewModelJson);
        return View(articlesViewModel);
    }
}
