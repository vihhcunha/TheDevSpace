using Bogus;
using TheDevSpace.Domain.DomainExceptions;
using TheDevSpace.Domain.Entities;

namespace TheDevSpaceTests.Domain.Entities;

public class WriterTests
{
    [Fact(DisplayName = "Build writer object")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_BuildWriterObject_ShouldBuildCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var name = faker.Person.FullName;
        var age = faker.Random.Number();
        var description = faker.Lorem.Sentence();
        var role = faker.Lorem.Paragraph();
        var userId = Guid.NewGuid();

        //Act
        var writer = new Writer(name, age, description, role, userId);

        // Assert
        Assert.IsType<Guid>(writer.WriterId);
        Assert.IsType<DateTime>(writer.RegistrationDateTime);
        Assert.Equal(name, writer.Name);
        Assert.Equal(age, writer.Age);
        Assert.Equal(description, writer.Description);
        Assert.Equal(role, writer.Role);
    }

    [Fact(DisplayName = "Build writer object - without name")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_BuildWriterObjectWithoutName_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Writer("", 20, "Short description", "Software Engineer", Guid.NewGuid()));
    }

    [Fact(DisplayName = "Build writer object - without description")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_BuildWriterObjectWithoutDescription_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Writer("Vinicius", 20, "", "Software Engineer", Guid.NewGuid()));
    }

    [Fact(DisplayName = "Build writer object - without role")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_BuildWriterObjectWithoutRole_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Writer("Vinicius", 20, "Short Description", "", Guid.NewGuid()));
    }
}
