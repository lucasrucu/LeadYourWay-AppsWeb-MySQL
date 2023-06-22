using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Moq;

namespace LeadYourWay.Domain.Test;

public class CardDomainUnitTest {
    [Fact]
    public void TestCardCreation() {
    
    Card card = new Card() {
        Name = "Mi Tarjeta",
        Number = "1234567890123456",
        Cvv = "123",
        ExpirationDate = DateTime.Now.AddDays(5),
        Type = "VISA",
        UserId = 1
    };
    var userInfrastructure = new Mock<IUserInfrastructure>();
    var cardInfrastructure = new Mock<ICardInfrastructure>();
    cardInfrastructure.Setup(x => x.save(card)).Returns(true);
    userInfrastructure.Setup(x => x.ExistsById(card.UserId)).Returns(true);
    CardDomain cardDomain = new CardDomain(cardInfrastructure.Object, userInfrastructure.Object);

    // Act
    var result = cardDomain.save(card);

    // Assert
    Assert.True(result);
    }
    
    [Fact]
    public void TestEmptyType() {
    
        Card card = new Card() {
            Name = "Mi Tarjeta",
            Number = "1234567890123456",
            Cvv = "123",
            ExpirationDate = DateTime.Now.AddDays(5),
            Type = "",
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var cardInfrastructure = new Mock<ICardInfrastructure>();
        cardInfrastructure.Setup(x => x.save(card)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(card.UserId)).Returns(true);
        CardDomain cardDomain = new CardDomain(cardInfrastructure.Object, userInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => cardDomain.save(card));
    }

    
    [Fact]
    public void TestInvalidCvv() {
    
        Card card = new Card() {
            Name = "Mi Tarjeta",
            Number = "1234567890123456",
            Cvv = "12345",
            ExpirationDate = DateTime.Now.AddDays(5),
            Type = "VISA",
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var cardInfrastructure = new Mock<ICardInfrastructure>();
        cardInfrastructure.Setup(x => x.save(card)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(card.UserId)).Returns(true);
        CardDomain cardDomain = new CardDomain(cardInfrastructure.Object, userInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => cardDomain.save(card));
    }

   
    [Fact]
    public void TestInvalidName() {
    
        Card card = new Card() {
            Name = "",
            Number = "1234567890123456",
            Cvv = "123",
            ExpirationDate = DateTime.Now.AddDays(5),
            Type = "VISA",
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var cardInfrastructure = new Mock<ICardInfrastructure>();
        cardInfrastructure.Setup(x => x.save(card)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(card.UserId)).Returns(true);
        CardDomain cardDomain = new CardDomain(cardInfrastructure.Object, userInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => cardDomain.save(card));
    }

    
    [Fact]
    public void TestUserNotFound() {
    
        Card card = new Card() {
            Name = "Mi Tarjeta",
            Number = "1234567890123456",
            Cvv = "123",
            ExpirationDate = DateTime.Now.AddDays(5),
            Type = "VISA",
            UserId = 1
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        var cardInfrastructure = new Mock<ICardInfrastructure>();
        cardInfrastructure.Setup(x => x.save(card)).Returns(true);
        userInfrastructure.Setup(x => x.ExistsById(card.UserId)).Returns(false);
        CardDomain cardDomain = new CardDomain(cardInfrastructure.Object, userInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => cardDomain.save(card));
    }

}