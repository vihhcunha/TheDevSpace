using AutoMapper;
using TheDevSpace.Application.Validation;
using TheDevSpace.Application.ValidationService;
using TheDevSpace.Domain.Entities;
using TheDevSpace.Repository;
using TheDevSpace.Repository.Repository;

namespace TheDevSpace.Application;

public class ArticleService : ServiceBase, IArticleService
{
    private readonly IMapper _mapper;
    private readonly IArticleRepository _articleRepository;

    public ArticleService(
        IMapper mapper, 
        IArticleRepository articleRepository, 
        IValidationService validationService) : base(validationService)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
    }

    public async Task AddArticle(ArticleDto articleDto)
    {
        if (articleDto == null) throw new ArgumentNullException(nameof(articleDto));
        if (!ExecuteValidation(new ArticleValidation(), articleDto)) return;

        var article = new Article(articleDto.Title, articleDto.Content, articleDto.WriterId, articleDto.Description);
        await _articleRepository.AddArticle(article);
        await _articleRepository.UnitOfWork.SaveChangesAsync();
    }
    public async Task EditArticle(ArticleDto articleDto)
    {
        if (articleDto == null) throw new ArgumentNullException(nameof(articleDto));
        if (!ExecuteValidation(new ArticleValidation(), articleDto)) return;

        var article = await _articleRepository.GetById(articleDto.ArticleId);
        if (article == null) throw new Exception("This article does not exists!");

        article.UpdateData(articleDto.Title, articleDto.Description, articleDto.Content);

        await _articleRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task AddStar(Guid userId, Guid articleId)
    {
        var article = await _articleRepository.GetArticleWithStars(articleId);
        if (article == null)
        {
            Notificate("This article does not exists to give a star");
            return;
        }

        article.GiveStar(userId);

        await _articleRepository.AddStar(article.Stars.LastOrDefault());
        await _articleRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteArticle(Guid articleId)
    {
        await _articleRepository.DeleteArticle(articleId);
        await _articleRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteStar(Guid userId, Guid articleId)
    {
        var star = await _articleRepository.GetArticleStarByArticleAndUser(userId, articleId);

        if (star == null)
        {
            Notificate("This star does not exists!");
            return;
        }

        await _articleRepository.DeleteStar(star);
        await _articleRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task<List<ArticleDto>> GetAllArticles()
    {
        var articles = await _articleRepository.GetAllArticles();

        return _mapper.Map<List<ArticleDto>>(articles);
    }

    public async Task<List<ArticleDto>> GetArticlesByWriter(Guid writerId)
    {
        var articles = await _articleRepository.GetArticlesByWriter(writerId);

        return _mapper.Map<List<ArticleDto>>(articles);
    }

    public async Task<List<ArticleDto>> GetArticlesByWriter(Guid writerId, string search)
    {
        var articles = await _articleRepository.GetArticlesByWriter(writerId, search);

        return _mapper.Map<List<ArticleDto>>(articles);
    }

    public async Task<ArticleStarDto> GetArticleStar(Guid articleStarId)
    {
        var star = await _articleRepository.GetArticleStar(articleStarId);

        return _mapper.Map<ArticleStarDto>(star);
    }

    public async Task<ArticleDto> GetArticleWithStars(Guid articleId)
    {
        var article = await _articleRepository.GetArticleWithStars(articleId);

        return _mapper.Map<ArticleDto>(article);
    }

    public async Task<List<ArticleDto>> SearchArticlesByTitle(string search)
    {
        var articles = await _articleRepository.SearchArticlesByTitle(search);

        return _mapper.Map<List<ArticleDto>>(articles);
    }
}
