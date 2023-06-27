using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Moq;

namespace LeadYourWay.Domain.Test;

public class BicycleDomainUnitTest
{
    [Fact]
    public void TestBicycleCreation()
    {
        Bicycle bicycle = new Bicycle()
        {
            Name = "Mi Bicicleta",
            Description = "Mi Bicicleta",
            Price = 100,
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var bicycleInfrastructure = new Mock<IBicycleInfrastructure>();
        var rentInfrastructure = new Mock<IRentInfrastructure>();
        bicycleInfrastructure.Setup(x => x.save(bicycle)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(bicycle.UserId)).Returns(true);
        BicycleDomain bicycleDomain = new BicycleDomain(bicycleInfrastructure.Object, userInfrastructure.Object, rentInfrastructure.Object);

        // Act
        var result = bicycleDomain.save(bicycle);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestEmptyName()
    {
        Bicycle bicycle = new Bicycle()
        {
            Name = "",
            Description = "Mi Bicicleta",
            Price = 100,
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var bicycleInfrastructure = new Mock<IBicycleInfrastructure>();
        var rentInfrastructure = new Mock<IRentInfrastructure>();
        bicycleInfrastructure.Setup(x => x.save(bicycle)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(bicycle.UserId)).Returns(true);
        BicycleDomain bicycleDomain = new BicycleDomain(bicycleInfrastructure.Object, userInfrastructure.Object, rentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => bicycleDomain.save(bicycle));
    }
    [Fact]
    public void TestLargeName()
    {
        Bicycle bicycle = new Bicycle()
        {
            Name = "12345678901234567890123456789012345678901234567890123456789012345678901234567890",
            Description = "Mi Bicicleta",
            Price = 100,
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var bicycleInfrastructure = new Mock<IBicycleInfrastructure>();
        var rentInfrastructure = new Mock<IRentInfrastructure>();
        bicycleInfrastructure.Setup(x => x.save(bicycle)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(bicycle.UserId)).Returns(true);
        BicycleDomain bicycleDomain = new BicycleDomain(bicycleInfrastructure.Object, userInfrastructure.Object, rentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => bicycleDomain.save(bicycle));
    }
    
    [Fact]
    public void TestNegativePrice()
    {
        Bicycle bicycle = new Bicycle()
        {
            Name = "Mi Bicicleta",
            Description = "Mi Bicicleta",
            Price = -100,
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var bicycleInfrastructure = new Mock<IBicycleInfrastructure>();
        var rentInfrastructure = new Mock<IRentInfrastructure>();
        bicycleInfrastructure.Setup(x => x.save(bicycle)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(bicycle.UserId)).Returns(true);
        BicycleDomain bicycleDomain = new BicycleDomain(bicycleInfrastructure.Object, userInfrastructure.Object, rentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => bicycleDomain.save(bicycle));
    }
    
    [Fact]
    public void TestUserNotFound()
    {
        Bicycle bicycle = new Bicycle()
        {
            Name = "Mi Bicicleta",
            Description = "Mi Bicicleta",
            Price = 100,
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var bicycleInfrastructure = new Mock<IBicycleInfrastructure>();
        var rentInfrastructure = new Mock<IRentInfrastructure>();
        bicycleInfrastructure.Setup(x => x.save(bicycle)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(bicycle.UserId)).Returns(false);
        BicycleDomain bicycleDomain = new BicycleDomain(bicycleInfrastructure.Object, userInfrastructure.Object, rentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => bicycleDomain.save(bicycle));
    }
}