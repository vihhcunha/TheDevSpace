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
        var description = faker.Lorem.Text();
        var writerId = Guid.NewGuid();

        //Act
        var article = new Article(title, content, writerId, description);

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
        Assert.Throws<DomainException>(() => new Article("Random title", "", Guid.NewGuid(), "Random description"));
    }

    [Fact(DisplayName = "Build article object - without title")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_BuildArticleObjectWithoutTitle_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Article("", "Random content", Guid.NewGuid(), "Random description"));
    }

    [Fact(DisplayName = "Build article object - without description")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_BuildArticleObjectWithoutDescription_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Article("Random title", "Random content", Guid.NewGuid(), ""));
    }

    [Fact(DisplayName = "Give article star")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_CreateArticleStar_ShouldCreateCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var title = faker.Lorem.Sentence();
        var content = faker.Lorem.Text();
        var description = faker.Lorem.Text();
        var writerId = Guid.NewGuid();

        var article = new Article(title, content, writerId, description);

        //Act & Assert
        article.GiveStar(Guid.NewGuid());
        Assert.Single(article.Stars);

        article.GiveStar(Guid.NewGuid());
        Assert.Equal(2, article.Stars.Count);
    }

    [Fact(DisplayName = "Update article data")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_UpdateArticleData_ShouldUpdateCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var title = faker.Lorem.Sentence();
        var content = faker.Lorem.Text();
        var description = faker.Lorem.Text();
        var writerId = Guid.NewGuid();

        var article = new Article(title, content, writerId, description);

        title = faker.Lorem.Sentence();
        content = faker.Lorem.Text();
        description = faker.Lorem.Text();

        //Act
        article.UpdateData(title, description, content);

        //Assert
        Assert.Equal(title, article.Title);
        Assert.Equal(description, article.Description);
        Assert.Equal(content, article.Content);
    }

    [Fact(DisplayName = "Update article data - validate empty title")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_UpdateArticleDataWithEmptyTitle_ShouldReceiveException()
    {
        //Arrange
        var faker = new Faker();

        var title = faker.Lorem.Sentence();
        var content = faker.Lorem.Text();
        var description = faker.Lorem.Text();
        var writerId = Guid.NewGuid();

        var article = new Article(title, content, writerId, description);

        //Act & Assert
        Assert.Throws<DomainException>(() => article.UpdateData("", description, content));
    }

    [Fact(DisplayName = "Update article data - validate empty description")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_UpdateArticleDataWithEmptyDescription_ShouldReceiveException()
    {
        //Arrange
        var faker = new Faker();

        var title = faker.Lorem.Sentence();
        var content = faker.Lorem.Text();
        var description = faker.Lorem.Text();
        var writerId = Guid.NewGuid();

        var article = new Article(title, content, writerId, description);

        //Act & Assert
        Assert.Throws<DomainException>(() => article.UpdateData(title, "", content));
    }

    [Fact(DisplayName = "Update article data - validate empty content")]
    [Trait("Category", "Domain.Article")]
    public void ArticleTests_UpdateArticleDataWithEmptyContent_ShouldReceiveException()
    {
        //Arrange
        var faker = new Faker();

        var title = faker.Lorem.Sentence();
        var content = faker.Lorem.Text();
        var description = faker.Lorem.Text();
        var writerId = Guid.NewGuid();

        var article = new Article(title, content, writerId, description);

        //Act & Assert
        Assert.Throws<DomainException>(() => article.UpdateData(title, description, ""));
    }
}
