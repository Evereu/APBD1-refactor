using LegacyApp;

namespace LegacyAppTests;

public class UnitTest1
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot() 
    {
        //Arrange
        string firstName = "Jan";
        string lastName = "Kowalski";
        string email = "asdpl";
        DateTime dateOfBirth = new DateTime(1998, 10,10);
        int clientId = 5;
        var service = new UserService();

        //Act
        bool result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        //Assert
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Or_LastName_Is_Null_Or_Empty() 
    {
        //Arrange
        string firstName = "";
        string lastName = null;
        string email = "jan.kowalski@gmail.com";
        DateTime dateOfBirth = new DateTime(1998, 10,10);
        int clientId = 5;
        var service = new UserService();

        //Act
        bool result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        //Assert
        Assert.Equal(false, result);
    }
    
    
    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_lower_Than_21()     
    {
        //Arrange
        string firstName = "Jan";
        string lastName = "Kowalski";
        string email = "jan.kowalski@gmail.com";
        DateTime dateOfBirth = new DateTime(2015, 10,10);
        int clientId = 5;
        var service = new UserService();

        //Act
        bool result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        //Assert
        Assert.Equal(false, result);
    }
}