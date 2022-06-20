using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheDevSpace.Application;
using TheDevSpace.Application.ValidationService;
using TheDevSpaceWebApp.Services;
using TheDevSpaceWebApp.ViewModels.Article;

namespace TheDevSpaceWebApp.Controllers;

public class ArticlesController : BaseController
{
    private readonly IArticleService _articleService;
    private readonly IAuthenticationService _authenticationService;
    public ArticlesController(IValidationService validationService, IArticleService articleService, IAuthenticationService authenticationService) : base(validationService)
    {
        _articleService = articleService;
        _authenticationService = authenticationService;
    }

    [Authorize()]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(NewArticleViewModel newArticleViewModel)
    {
        if (!ModelState.IsValid) return View(newArticleViewModel);
        if (_authenticationService.IsWriter == false)
        {
            ModelState.AddModelError(string.Empty, "You are not a writer, then you can't write a article!");
            return View(newArticleViewModel);
        }

        await _articleService.AddArticle(new ArticleDto
        {
            Content = newArticleViewModel.Content,
            Title = newArticleViewModel.Title,
            WriterId = _authenticationService.WriterId.Value,
            Description = newArticleViewModel.Description
        });

        if (IsInvalidOperation()) return View(newArticleViewModel);

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task<IActionResult> MyArticles()
    {
        if (_authenticationService.IsWriter == false)
        {
            ModelState.AddModelError(String.Empty, "You are not a writer.");
            return View();
        }

        var articles = await _articleService.GetArticlesByWriter(_authenticationService.WriterId.Value);
        var articlesViewModel = articles.Select(a => new ArticleViewModel
        {
            ArticleId = a.ArticleId,
            Content = a.Content,
            Description = a.Description,
            Launch = a.Launch,
            StarsCount = a.Stars.Count,
            Title = a.Title,
            WriterId = a.WriterId,
            WriterName = a.Writer.User.Name
        }).ToList();
        return View(articlesViewModel);
    }
}
