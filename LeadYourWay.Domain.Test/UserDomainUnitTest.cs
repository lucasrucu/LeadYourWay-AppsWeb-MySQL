using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Moq;

namespace LeadYourWay.Domain.Test;

public class UserDomainUnitTest {

    [Fact]
    public void TestUnderageUser() {
        // Arrange
        User user = new User() {
        Name = "Mbappe",
        Email = "mbappe@mail.com",
        Password = "1234",
        Phone = "123456789",
        BirthDate = DateTime.Now.AddYears(-14),
        Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Ftwitter.com%2FF_GallardoTV%2Fstatus%2F1666367311240986625&psig=AOvVaw3IC6GFESjc2XuVeP_Cc8Bg&ust=1687547795609000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCICD7MnL1_8CFQAAAAAdAAAAABAE",
    };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        userInfrastructure.Setup(t => t.save(user)).Returns(true);
        UserDomain userDomain = new UserDomain(userInfrastructure.Object);

        Assert.Throws<Exception>(() => userDomain.save(user));
    }
    
    [Fact]
    public void TestInvalidName() {
        // Arrange
        User user = new User() {
            Name = "Mbappeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
            Email = "mbappe@mail.com",
            Password = "1234",
            Phone = "123456789",
            BirthDate = DateTime.Now.AddYears(-23),
            Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Ftwitter.com%2FF_GallardoTV%2Fstatus%2F1666367311240986625&psig=AOvVaw3IC6GFESjc2XuVeP_Cc8Bg&ust=1687547795609000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCICD7MnL1_8CFQAAAAAdAAAAABAE",
        };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        userInfrastructure.Setup(t => t.save(user)).Returns(true);
        UserDomain userDomain = new UserDomain(userInfrastructure.Object);

        Assert.Throws<Exception>(() => userDomain.save(user));
    }
 

    [Fact]
    public void TestUserCreation() {
        // Arrange
        User user = new User() {
        Name = "Mbappe",
        Email = "mbappe@mail.com",
        Password = "1234",
        Phone = "123456789",
        BirthDate = DateTime.Now.AddYears(-23),
        Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Ftwitter.com%2FF_GallardoTV%2Fstatus%2F1666367311240986625&psig=AOvVaw3IC6GFESjc2XuVeP_Cc8Bg&ust=1687547795609000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCICD7MnL1_8CFQAAAAAdAAAAABAE",
    };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        userInfrastructure.Setup(t => t.save(user)).Returns(true);
        UserDomain userDomain = new UserDomain(userInfrastructure.Object);

        // Act
        var result = userDomain.save(user);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public void TestNameIsEmpty() {
        // Arrange
        User user
            = new User()
            {
                Name = "",
                Email = "mbappe@mail.com",
                Password = "1234",
                Phone = "123456789",
                BirthDate = DateTime.Now.AddYears(-23),
                Image = "https://upload.wikimedia.org/wikipedia/commons/7/72/Gonzalo_Higua%C3%ADn_2019.jpg",
            };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        userInfrastructure.Setup(t => t.save(user)).Returns(true);
        UserDomain userDomain = new UserDomain(userInfrastructure.Object);
        Assert.Throws<Exception>(() => userDomain.save(user));
    }
    [Fact]
    public void TestEmail() {
        // Arrange
        User user
            = new User()
            {
                Name = "Mbappe",
                Email = "mbappe@mail.comfdfsafdsadfksjafaslkdjflaskjflsajfdlasdjf",
                Password = "1234",
                Phone = "123456789",
                BirthDate = DateTime.Now.AddYears(-23),
                Image = "https://upload.wikimedia.org/wikipedia/commons/7/72/Gonzalo_Higua%C3%ADn_2019.jpg",
            };
        var userInfrastructure = new Mock<IUserInfrastructure>();
        userInfrastructure.Setup(t => t.save(user)).Returns(true);
        UserDomain userDomain = new UserDomain(userInfrastructure.Object);
        Assert.Throws<Exception>(() => userDomain.save(user));
    }

}