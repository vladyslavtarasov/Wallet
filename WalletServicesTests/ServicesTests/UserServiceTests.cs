namespace WalletServicesTests.ServicesTests;

[TestFixture]
public class UserServiceTests
{
    private static readonly UserRepositoryMock Users = new();
    private static readonly AccountRepositoryMock Accounts = new();
    private static readonly ExpenseCategoryRepositoryMock ExpenseCategories = new();
    private static readonly ExpenseStatementRepositoryMock ExpenseStatements = new();
    private static readonly IncomeCategoryRepositoryMock IncomeCategories = new();
    private static readonly IncomeStatementRepositoryMock IncomeStatements = new();

    private static readonly UnitOfWorkMock UnitOfWork = new(Accounts, Users,
        ExpenseCategories, ExpenseStatements,
        IncomeCategories, IncomeStatements);

    private readonly UserService _userService = new(UnitOfWork);

    [Test]
    public void RegisterUser_WithCorrectData_RegistersUser()
    {
        var name = "name";
        var surname = "surname";
        var userName = "userName";
        var password = "password";
        var isRegistered = _userService.RegisterUser(name, surname, userName, password);

        var user = Users.GetAll().Where(u => u.Name.Equals(name)
                                             && u.Password.Equals(password)
                                             && u.Surname.Equals(surname)
                                             && u.UserName.Equals(userName)).ToList();
        
        Assert.Multiple(() =>
        {
            Assert.True(isRegistered); 
            Assert.True(user.Count == 1 && user[0].Name.Equals(name) && user[0].Surname.Equals(surname) 
                        && user[0].UserName.Equals(userName) && user[0].Password.Equals(password));
        });
    }
    
    [Test]
    public void RegisterUser_WithNotCorrectData_TrowsArgumentException()
    {
        var name = "name";
        var surname = "surname";
        var wrongUserName = "user1";
        var password = "password";

        Assert.Throws<ArgumentException>(() => _userService.RegisterUser(name, surname, wrongUserName, password));
    }
    
    [Test]
    public void Login_WithCorrectData_Logins()
    {
        var userName = "user1";
        var password = "user1";
        
        var isLoggedIn = _userService.Login(userName, password);
        
        Assert.True(isLoggedIn);
    }
    
    [Test]
    public void Login_WithWrongUsername_TrowsArgumentException()
    {
        var wrongUserName = "user";
        var password = "user1";
        
        Assert.Throws<ArgumentException>(() => _userService.Login(wrongUserName, password));
    }
    
    [Test]
    public void Login_WithWrongPassword_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongPassword = "user";
        
        Assert.Throws<ArgumentException>(() => _userService.Login(userName, wrongPassword));
    }

    [Test]
    public void GetName_WithCorrectData_ReturnsName()
    {
        var userName = "user1";
        var password = "user1";
        var expectedName = "user1";
        
        var name = _userService.GetName(userName, password);

        Assert.That(expectedName, Is.EqualTo(name));
    }
    
    [Test]
    public void GetName_WithWrongUsername_TrowsArgumentException()
    {
        var wrongUserName = "user";
        var password = "user1";
        
        Assert.Throws<ArgumentException>(() => _userService.GetName(wrongUserName, password));
    }
    
    [Test]
    public void GetName_WithWrongPassword_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongPassword = "user";
        
        Assert.Throws<ArgumentException>(() => _userService.GetName(userName, wrongPassword));
    }
    
    [Test]
    public void GetSurname_WithCorrectData_ReturnsName()
    {
        var userName = "user1";
        var password = "user1";
        var expectedSurname = "user1";
        
        var name = _userService.GetSurname(userName, password);
        
        Assert.That(expectedSurname, Is.EqualTo(name));
    }
    
    [Test]
    public void GetSurname_WithWrongUsername_TrowsArgumentException()
    {
        var wrongUserName = "user";
        var password = "user1";
        
        Assert.Throws<ArgumentException>(() => _userService.GetSurname(wrongUserName, password));
    }
    
    [Test]
    public void GetSurname_WithWrongPassword_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongPassword = "user";
        
        Assert.Throws<ArgumentException>(() => _userService.GetSurname(userName, wrongPassword));
    }
}