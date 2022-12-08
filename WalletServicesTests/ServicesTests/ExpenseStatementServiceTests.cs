namespace WalletServicesTests.ServicesTests;

[TestFixture]
public class ExpenseStatementServiceTests
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

    private readonly ExpenseStatementService _expenseStatementService = new(UnitOfWork);
    
    [Test]
    public void CreateExpenseStatement_WithCorrectData_CreatesExpenseStatement()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "ExpenseCategory1";
        var amount = 10m;
        
        var isCreated = _expenseStatementService.CreateExpenseStatement(userName, accountName, categoryName, amount);

        var statement = ExpenseStatements.GetAll()
            .Where(s => s.ExpenseCategory.Account.Name.Equals(accountName)
                        && s.ExpenseCategory.Account.User.Name.Equals(userName)
                        && s.ExpenseCategory.Name.Equals(categoryName)
                        && s.Amount.Equals(amount)).ToList();
                                                   
        
        Assert.Multiple(() =>
        {
            Assert.That(isCreated, Is.True);
            Assert.True(statement.Count == 1 && statement[0].ExpenseCategory.Account.Name.Equals(accountName)
                                             && statement[0].ExpenseCategory.Account.User.Name.Equals(userName)
                                             && statement[0].ExpenseCategory.Name.Equals(categoryName) 
                                             && statement[0].Amount.Equals(amount));
        });
    }
    
    [Test]
    public void CreateExpenseStatement_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "ExpenseCategory1";
        var amount = 10m;

        Assert.Throws<ArgumentException>(() =>
            _expenseStatementService.CreateExpenseStatement(wrongUserName, accountName, categoryName, amount));
    }
    
    [Test]
    public void CreateExpenseStatement_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "ExpenseCategory1";
        var amount = 10m;

        Assert.Throws<ArgumentException>(() =>
            _expenseStatementService.CreateExpenseStatement(userName, wrongAccountName, categoryName, amount));
    }
    
    [Test]
    public void CreateExpenseStatement_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "ExpenseCategory10";
        var amount = 10m;

        Assert.Throws<ArgumentException>(() =>
            _expenseStatementService.CreateExpenseStatement(userName, accountName, wrongCategoryName, amount));
    }
}