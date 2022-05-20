using Bogus;
using TheDevSpace.Domain.DomainExceptions;
using TheDevSpace.Domain.Entities;

namespace TheDevSpaceTests.Domain.Entities;

public class ArticleTests
{
    [Fact(DisplayName = "Build article object")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_BuildArticleObject_ShouldBuildCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var title = faker.Lorem.Sentence();
        var content = faker.Lorem.Text();
        var writerId = Guid.NewGuid();

        //Act
        var article = new Article(title, content, writerId);

        // Assert
        Assert.IsType<Guid>(article.ArticleId);
        Assert.IsType<DateTime>(article.Launch);
        Assert.Equal(writerId, article.WriterId);
        Assert.Equal(title, article.Title);
        Assert.Equal(content, article.Content);
    }

    [Fact(DisplayName = "Build article object - without content")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_BuildArticleObjectWithoutContent_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Article("Random title", "", Guid.NewGuid()));
    }

    [Fact(DisplayName = "Build article object - without title")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_BuildArticleObjectWithoutTitle_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Article("", "Random content", Guid.NewGuid()));
    }

    [Fact(DisplayName = "Give article star")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_CreateArticleStar_ShouldCreateCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var title = faker.Lorem.Sentence();
        var content = faker.Lorem.Text();
        var writerId = Guid.NewGuid();
        var article = new Article(title, content, writerId);

        //Act & Assert
        article.GiveStar(Guid.NewGuid());
        Assert.Single(article.Stars);

        article.GiveStar(Guid.NewGuid());
        Assert.Equal(2, article.Stars.Count);
    }
}
