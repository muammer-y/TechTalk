using Application.Features.Users.Commands.CreateUser;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Tests.Users;

/// <summary>
/// TUnit
/// <see cref="https://github.com/thomhurst/TUnit"/>
/// </summary>
public class CreateUserCommandTests
{
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IPasswordHasher _passwordHasherMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _passwordHasherMock = new PasswordHasher();

        _handler = new(_unitOfWorkMock, _passwordHasherMock);
    }

    [Test]
    public async Task Handler_ShouldReturnError_WhenEmailExists()
    {
        //Arrange
        CreateUserCommand command = new("test@test.com", "Test", "Muammer", "Yılmaz");

        _unitOfWorkMock.UserRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(UserTestData.ValidUser);

        //Act
        var response = await _handler.Handle(command, default);

        //Assert
        await Assert.That(response).IsNotNull();
        await Assert.That(response.IsSuccess).IsFalse();
        await Assert.That(response.Error).IsNotNull();
    }

    [Test]
    public async Task Handler_ShouldCreateUser()
    {
        CreateUserCommand command = new("test@test.com", "Test", "Muammer", "Yılmaz");
        _unitOfWorkMock.UserRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(UserTestData.NullUser);

        var response = await _handler.Handle(command, default);

        await Assert.That(response).IsNotNull();
        await Assert.That(response.IsSuccess).IsTrue();
        await Assert.That(response.Error).IsNull();
    }
}

public static class UserTestData
{
    public static User ValidUser => new()
    {
        Email = "test@test.com",
        FirstName = "Test",
        LastName = "Test",
        Password = "Test1234"
    };

    public static User? NullUser => null;
}
