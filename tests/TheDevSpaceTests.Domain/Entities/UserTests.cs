using Bogus;
using TheDevSpace.Domain.DomainExceptions;
using TheDevSpace.Domain.Entities;

namespace TheDevSpaceTests.Domain.Entities;

public class UserTests
{
    [Fact(DisplayName = "Build user object")]
    [Trait("Category", "Domain.User")]
    public void UserTests_BuildUserObject_ShouldBuildCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var email = faker.Internet.Email();
        var password = faker.Internet.Password();

        //Act
        var user = new User(email, password);

        // Assert
        Assert.IsType<Guid>(user.UserId);
        Assert.IsType<DateTime>(user.RegistrationDateTime);
        Assert.Equal(email, user.Email);
        Assert.Equal(password, user.Password);
    }

    [Fact(DisplayName = "Build user object - without email")]
    [Trait("Category", "Domain.User")]
    public void UserTests_BuildUserObjectWithoutEmail_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new User("", "Random"));
    }

    [Fact(DisplayName = "Build user object - with invalid email")]
    [Trait("Category", "Domain.User")]
    public void UserTests_BuildUserObjectWithInvalidEmail_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new User("aaa", "Random"));
        Assert.Throws<DomainException>(() => new User("aaa.aaa", "Random"));
    }
}
