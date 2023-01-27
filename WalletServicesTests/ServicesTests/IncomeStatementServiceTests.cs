namespace WalletServicesTests.ServicesTests;

[TestFixture]
public class IncomeStatementServiceTests
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

    private readonly IncomeStatementService _incomeStatementService = new(UnitOfWork);
    
    [Test]
    public void CreateIncomeStatement_WithCorrectData_CreatesIncomeStatement()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "IncomeCategory1";
        var amount = 10m;
        
        var isCreated = _incomeStatementService.CreateIncomeStatement(userName, accountName, categoryName, amount);

        var statement = IncomeStatements.GetAll()
            .Where(s => s.IncomeCategory.Account.Name.Equals(accountName)
                        && s.IncomeCategory.Account.User.Name.Equals(userName)
                        && s.IncomeCategory.Name.Equals(categoryName)
                        && s.Amount.Equals(amount)).ToList();
                                                   
        
        Assert.Multiple(() =>
        {
            Assert.That(isCreated, Is.True);
            Assert.True(statement.Count == 1 && statement[0].IncomeCategory.Account.Name.Equals(accountName)
                                             && statement[0].IncomeCategory.Account.User.Name.Equals(userName)
                                             && statement[0].IncomeCategory.Name.Equals(categoryName) 
                                             && statement[0].Amount.Equals(amount));
        });
    }
    
    [Test]
    public void CreateIncomeStatement_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "IncomeCategory1";
        var amount = 10m;

        Assert.Throws<ArgumentException>(() =>
            _incomeStatementService.CreateIncomeStatement(wrongUserName, accountName, categoryName, amount));
    }
    
    [Test]
    public void CreateIncomeStatement_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "IncomeCategory1";
        var amount = 10m;

        Assert.Throws<ArgumentException>(() =>
            _incomeStatementService.CreateIncomeStatement(userName, wrongAccountName, categoryName, amount));
    }
    
    [Test]
    public void CreateIncomeStatement_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "IncomeCategory10";
        var amount = 10m;

        Assert.Throws<ArgumentException>(() =>
            _incomeStatementService.CreateIncomeStatement(userName, accountName, wrongCategoryName, amount));
    }
}