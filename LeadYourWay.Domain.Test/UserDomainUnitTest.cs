using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Moq;

namespace LeadYourWay.Domain.Test;

public class UserDomainUnitTest
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var user = new User()
        {
            Name = "Test"
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var encryptDomain = new Mock<IEncryptDomain>();
        var tokenDomain = new Mock<ITokenDomain>();
        userInfrastructure.Setup(t => t.save(user)).Returns(true);
        var userDomain = new UserDomain(userInfrastructure.Object, encryptDomain.Object, tokenDomain.Object);

        // Act
        var result = userDomain.save(user);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Test2()
    {
        // Arrange
        var user = new User()
        {
            Name = "Test123456789123456789123456789123456789123456789123"
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var encryptDomain = new Mock<IEncryptDomain>();
        var tokenDomain = new Mock<ITokenDomain>();
        //userInfrastructure.Setup(t => t.save(user)).Returns(true);
        var userDomain = new UserDomain(userInfrastructure.Object, encryptDomain.Object, tokenDomain.Object);

        // Act
        var ex = Assert.Throws<Exception>(() => userDomain.save(user));

        // Assert
        Assert.Equal("Name is not valid", ex.Message);
    }
}