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
    public async Task<IActionResult> Create(CreateEditArticleViewModel newArticleViewModel)
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

    [Authorize()]
    [HttpGet("Articles/Edit/{articleId}")]
    public async Task<IActionResult> Edit([FromRoute] Guid articleId)
    {
        if (_authenticationService.IsWriter == false)
        {
            ModelState.AddModelError(String.Empty, "You are not a writer.");
            return View();
        }

        var article = await _articleService.GetArticleWithStars(articleId);

        return View(new CreateEditArticleViewModel
        {
            ArticleId = articleId,
            Content = article.Content,
            Description = article.Description,
            Title = article.Title,
            WriterId = article.WriterId
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(CreateEditArticleViewModel newArticleViewModel)
    {
        if (!ModelState.IsValid) return View(newArticleViewModel);
        if (_authenticationService.IsWriter == false)
        {
            ModelState.AddModelError(string.Empty, "You are not a writer, then you can't write a article!");
            return View(newArticleViewModel);
        }

        await _articleService.EditArticle(new ArticleDto
        {
            Content = newArticleViewModel.Content,
            Title = newArticleViewModel.Title,
            WriterId = _authenticationService.WriterId.Value,
            Description = newArticleViewModel.Description,
            ArticleId = newArticleViewModel.ArticleId
        });

        if (IsInvalidOperation()) return View(newArticleViewModel);

        return RedirectToAction("Article", "Articles", new { articleId = newArticleViewModel.ArticleId });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> MyArticles([FromQuery] string? search)
    {
        if (_authenticationService.IsWriter == false)
        {
            ModelState.AddModelError(String.Empty, "You are not a writer.");
            return View();
        }

        List<ArticleDto> articles;
        if (search == null)
        {
            articles = await _articleService.GetArticlesByWriter(_authenticationService.WriterId.Value);
        }
        else
        {
            articles = await _articleService.GetArticlesByWriter(_authenticationService.WriterId.Value, search);
        }

        var articlesViewModel = ArticleViewModel.ArticlesToArticlesViewModel(articles);
        return View(articlesViewModel);
    }

    [HttpGet("Articles/Article/{articleId}")]
    [Authorize]
    public async Task<IActionResult> Article([FromRoute]Guid articleId)
    {
        var article = await _articleService.GetArticleWithStars(articleId);

        if (article == null)
        {
            ModelState.AddModelError(string.Empty, "This article does not exists.");
            return View();
        }
        var articleViewModel = ArticleViewModel.ArticleToArticleViewModel(article);
        articleViewModel.StarredByCurrentUser = UserAlreadyHaveStarredThisArticle(articleId, article);
        return View(articleViewModel);
    }

    [HttpGet("Articles/Delete/{articleId}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute]Guid articleId)
    {
        if (articleId == Guid.Empty) return RedirectToAction("MyArticles");

        await _articleService.DeleteArticle(articleId);

        AddValidationData();

        return RedirectToAction("MyArticles");
    }

    [HttpGet("Articles/GiveRemoveStar/{articleId}")]
    [Authorize]
    public async Task<IActionResult> GiveRemoveStar([FromRoute] Guid articleId)
    {
        if (articleId == Guid.Empty) return RedirectToAction("MyArticles");

        var article = await _articleService.GetArticleWithStars(articleId);
        if (UserAlreadyHaveStarredThisArticle(articleId, article))
        {
            await _articleService.DeleteStar(_authenticationService.UserId.GetValueOrDefault(), articleId);
        }
        else
        {
            await _articleService.AddStar(_authenticationService.UserId.GetValueOrDefault(), articleId);
        }

        return RedirectToAction("Article", new { articleId });
    }

    private bool UserAlreadyHaveStarredThisArticle(Guid articleId, ArticleDto article)
    {
        return article.Stars.Any(a => a.UserId == _authenticationService.UserId && a.ArticleId == articleId);
    }
}
