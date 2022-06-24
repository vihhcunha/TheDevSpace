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

        var age = faker.Random.Number(min: 1);
        var description = faker.Lorem.Sentence();
        var role = faker.Lorem.Paragraph();
        var userId = Guid.NewGuid();

        //Act
        var writer = new Writer(age, description, role, userId);

        // Assert
        Assert.IsType<Guid>(writer.WriterId);
        Assert.IsType<DateTime>(writer.RegistrationDateTime);
        Assert.Equal(age, writer.Age);
        Assert.Equal(description, writer.Description);
        Assert.Equal(role, writer.Role);
    }

    [Fact(DisplayName = "Build writer object - without description")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_BuildWriterObjectWithoutDescription_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Writer(20, "", "Software Engineer", Guid.NewGuid()));
    }

    [Fact(DisplayName = "Build writer object - without role")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_BuildWriterObjectWithoutRole_ShouldThrowError()
    {
        //Arrange, act & assert
        Assert.Throws<DomainException>(() => new Writer(20, "Short Description", "", Guid.NewGuid()));
    }

    [Fact(DisplayName = "Update writer")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_UpdateWriter_ShouldUpdateCorrectly()
    {
        //Arrange
        var faker = new Faker();

        var age = faker.Random.Number(min: 1);
        var description = faker.Lorem.Sentence();
        var role = faker.Lorem.Paragraph();
        var userId = Guid.NewGuid();

        var writer = new Writer(age, description, role, userId);

        age = faker.Random.Number(min: 1);
        description = faker.Lorem.Sentence();
        role = faker.Lorem.Paragraph();

        //Act
        writer.UpdateData(age, role, description);

        // Assert
        Assert.Equal(age, writer.Age);
        Assert.Equal(description, writer.Description);
        Assert.Equal(role, writer.Role);
    }

    [Fact(DisplayName = "Update writer - invalid age")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_UpdateWriterWithInvalidAge_ShouldThrowException()
    {
        //Arrange
        var faker = new Faker();

        var age = faker.Random.Number(min: 1);
        var description = faker.Lorem.Sentence();
        var role = faker.Lorem.Paragraph();
        var userId = Guid.NewGuid();

        var writer = new Writer(age, description, role, userId);

        //Act & Assert
        Assert.Throws<DomainException>(() => writer.UpdateData(-1, role, description));
    }

    [Fact(DisplayName = "Update writer - empty role")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_UpdateWriterWithEmptyRole_ShouldThrowException()
    {
        //Arrange
        var faker = new Faker();

        var age = faker.Random.Number(min: 1);
        var description = faker.Lorem.Sentence();
        var role = faker.Lorem.Paragraph();
        var userId = Guid.NewGuid();

        var writer = new Writer(age, description, role, userId);

        //Act & Assert
        Assert.Throws<DomainException>(() => writer.UpdateData(age, "", description));
    }

    [Fact(DisplayName = "Update writer - empty description")]
    [Trait("Category", "Domain.Writer")]
    public void WriterTests_UpdateWriterWithEmptyDescription_ShouldThrowException()
    {
        //Arrange
        var faker = new Faker();

        var age = faker.Random.Number(min: 1);
        var description = faker.Lorem.Sentence();
        var role = faker.Lorem.Paragraph();
        var userId = Guid.NewGuid();

        var writer = new Writer(age, description, role, userId);

        //Act & Assert
        Assert.Throws<DomainException>(() => writer.UpdateData(age, role, ""));
    }
}
