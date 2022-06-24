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

        var name = faker.Person.FullName;
        var email = faker.Internet.Email();
        var password = faker.Internet.Password();

        //Act
        var user = new User(email, password, name);

        // Assert
        Assert.IsType<Guid>(user.UserId);
        Assert.IsType<DateTime>(user.RegistrationDateTime);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(password, user.Password);
    }

    [Fact(DisplayName = "Build user object - without email")]
    [Trait("Category", "Domain.User")]
    public void UserTests_BuildUserObjectWithoutEmail_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new User("", "Random", "Vinicius"));
    }

    [Fact(DisplayName = "Build user object - without name")]
    [Trait("Category", "Domain.User")]
    public void UserTests_BuildUserObjectWithoutName_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new User("vinicius2010.cunha@hotmail.com", "Random", ""));
    }

    [Fact(DisplayName = "Build user object - with invalid email")]
    [Trait("Category", "Domain.User")]
    public void UserTests_BuildUserObjectWithInvalidEmail_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new User("aaa", "Random", "Vinicius"));
        Assert.Throws<DomainException>(() => new User("aaa.aaa", "Random", "Vinicius"));
    }

    [Fact(DisplayName = "Update user password")]
    [Trait("Category", "Domain.User")]
    public void UserTests_UpdateUserPassword_ShouldUpdateCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var name = faker.Person.FullName;
        var email = faker.Internet.Email();
        var password = faker.Internet.Password();

        var user = new User(email, password, name);

        password = faker.Internet.Password();

        //Act
        user.ChangePassword(password);

        // Assert
        Assert.Equal(password, user.Password);
    }

    [Fact(DisplayName = "Update user password - empty value")]
    [Trait("Category", "Domain.User")]
    public void UserTests_UpdateUserPasswordWithEmptyValue_ShouldThrowException()
    {
        //Arrange
        var faker = new Faker();

        var name = faker.Person.FullName;
        var email = faker.Internet.Email();
        var password = faker.Internet.Password();

        var user = new User(email, password, name);

        //Act & Assert
        Assert.Throws<DomainException>(() => user.ChangePassword(""));
    }

    [Fact(DisplayName = "Update user name")]
    [Trait("Category", "Domain.User")]
    public void UserTests_UpdateUserName_ShouldUpdateCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var name = faker.Person.FullName;
        var email = faker.Internet.Email();
        var password = faker.Internet.Password();

        var user = new User(email, password, name);

        name = faker.Person.FullName;

        //Act
        user.UpdateName(name);

        // Assert
        Assert.Equal(name, user.Name);
    }

    [Fact(DisplayName = "Update user name - empty value")]
    [Trait("Category", "Domain.User")]
    public void UserTests_UpdateUserNameWithEmptyValue_ShouldThrowException()
    {
        //Arrange
        var faker = new Faker();

        var name = faker.Person.FullName;
        var email = faker.Internet.Email();
        var password = faker.Internet.Password();

        var user = new User(email, password, name);

        //Act & Assert
        Assert.Throws<DomainException>(() => user.UpdateName(""));
    }
}
