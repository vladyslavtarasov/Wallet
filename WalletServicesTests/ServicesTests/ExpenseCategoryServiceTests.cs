namespace WalletServicesTests.ServicesTests;

[TestFixture]
public class ExpenseCategoryServiceTests
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

    private readonly ExpenseCategoryService _expenseCategoryService = new(UnitOfWork);
    
    [Test]
    public void CreateExpenseCategory_WithCorrectData_CreatesExpenseCategory()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "category";
        
        var isCreated = _expenseCategoryService.CreateExpenseCategory(userName, accountName, categoryName);
        
        var category = ExpenseCategories.GetAll().Where(c => c.Name.Equals(categoryName)
                                                             && c.Account.User.Name.Equals(userName)
                                                             && c.Account.Name.Equals(accountName)).ToList();
        
        Assert.Multiple(() =>
        {
            Assert.True(isCreated);
            Assert.True(category.Count == 1 && category[0].Name.Equals(categoryName) 
                                            && category[0].Account.Name.Equals(accountName) 
                                            && category[0].Account.User.Name.Equals(userName));
        });
    }
    
    [Test]
    public void CreateExpenseCategory_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "category";

        Assert.Throws<ArgumentException>(() =>
            _expenseCategoryService.CreateExpenseCategory(wrongUserName, accountName, categoryName));
    }
    
    [Test]
    public void CreateExpenseCategory_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "category";

        Assert.Throws<ArgumentException>(() =>
            _expenseCategoryService.CreateExpenseCategory(userName, wrongAccountName, categoryName));
    }
    
    [Test]
    public void CreateExpenseCategory_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "ExpenseCategory1";

        Assert.Throws<ArgumentException>(() =>
            _expenseCategoryService.CreateExpenseCategory(userName, accountName, wrongCategoryName));
    }

    [Test]
    public void DeleteExpenseCategory_WithCorrectData_DeletesExpenseCategoryAndChangesCategoryForTheStatementsToDefault()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "ExpenseCategory1";
        var statement = ExpenseStatements.GetAll()
            .FirstOrDefault(s => s.ExpenseCategory.Name.Equals(categoryName));
        
        var isDeleted = _expenseCategoryService.DeleteExpenseCategory(userName, accountName, categoryName);
        
        var category = ExpenseCategories.GetAll().Where(c => c.Name.Equals(categoryName)
                                                             && c.Account.User.Name.Equals(userName)
                                                             && c.Account.Name.Equals(accountName)).ToList();
        
        Assert.Multiple(() =>
        {
            Assert.True(isDeleted);
            Assert.That(statement!.ExpenseCategory.Name, Is.EqualTo(IUnitOfWork.DefaultCategory));
            Assert.True(category.Count == 0);
        });
    }
    
    [Test]
    public void DeleteExpenseCategory_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "ExpenseCategory1";

        Assert.Throws<ArgumentException>(() =>
            _expenseCategoryService.DeleteExpenseCategory(wrongUserName, accountName, categoryName));
    }
    
    [Test]
    public void DeleteExpenseCategory_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "ExpenseCategory1";

        Assert.Throws<ArgumentException>(() =>
            _expenseCategoryService.DeleteExpenseCategory(userName, wrongAccountName, categoryName));
    }
    
    [Test]
    public void DeleteExpenseCategory_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "ExpenseCategory10";

        Assert.Throws<ArgumentException>(() =>
            _expenseCategoryService.DeleteExpenseCategory(userName, accountName, wrongCategoryName));
    }
    
    [Test]
    public void DeleteExpenseCategory_WithDefaultCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var defaultCategoryName = IUnitOfWork.DefaultCategory;

        Assert.Throws<ArgumentException>(() =>
            _expenseCategoryService.DeleteExpenseCategory(userName, accountName, defaultCategoryName));
    }
    
    [Test]
    public void DeleteExpenseCategory_WithAccountWithoutDefaultCategory_TrowsArgumentException()
    {
        var userName = "user2";
        var accountName = "Account3";
        var categoryName = "ExpenseCategory2";

        Assert.Throws<NoDefaultCategoryException>(() =>
            _expenseCategoryService.DeleteExpenseCategory(userName, accountName, categoryName));
    }
    
    [Test]
    public void GetAccountExpenseCategories_WithCorrectData_ReturnsExpenseCategories()
    {
        var userName = "user1";
        var accountName = "Account1";
        var expectedCategories = ExpenseCategories.GetAll()
            .Where(s => s.Account.User.Name.Equals(userName)
                        && s.Account.Name.Equals(accountName));
        var expectedBusinessCategories = new List<BusinessExpenseCategory>();
        foreach (var expectedCategory in expectedCategories)
        {
            var businessExpenseCategory = new BusinessExpenseCategory()
            {
                AccountId = expectedCategory.AccountId,
                AccountName = expectedCategory.Account.Name,
                Id = expectedCategory.Id,
                Name = expectedCategory.Name
            };
            expectedBusinessCategories.Add(businessExpenseCategory);
        }

        var statements = _expenseCategoryService.GetAccountExpenseCategories(userName, accountName);
        
        Assert.That(statements, Is.EqualTo(expectedBusinessCategories));
    }
    
    [Test]
    public void GetAccountExpenseCategories_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";

        Assert.Throws<ArgumentException>(() => _expenseCategoryService.GetAccountExpenseCategories(wrongUserName, accountName));
    }
    
    [Test]
    public void GetAccountExpenseCategories_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";

        Assert.Throws<ArgumentException>(() => _expenseCategoryService.GetAccountExpenseCategories(userName, wrongAccountName));
    }
    
    [Test]
    public void GetDefaultCategoryName_ReturnsDefaultCategoryName()
    {
        var defaultCategory = _expenseCategoryService.GetDefaultCategoryName();
        
        Assert.That(defaultCategory, Is.EqualTo(IUnitOfWork.DefaultCategory));
    }
}