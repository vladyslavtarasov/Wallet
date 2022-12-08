namespace WalletServicesTests.ServicesTests;

[TestFixture]
public class IncomeCategoryServiceTests
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

    private readonly IncomeCategoryService _incomeCategoryService = new(UnitOfWork);
    
    [Test]
    public void CreateIncomeCategory_WithCorrectData_CreatesIncomeCategory()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "category";
        
        var isCreated = _incomeCategoryService.CreateIncomeCategory(userName, accountName, categoryName);
        
        var category = IncomeCategories.GetAll().Where(c => c.Name.Equals(categoryName)
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
    public void CreateIncomeCategory_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "category";

        Assert.Throws<ArgumentException>(() =>
            _incomeCategoryService.CreateIncomeCategory(wrongUserName, accountName, categoryName));
    }
    
    [Test]
    public void CreateIncomeCategory_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "category";

        Assert.Throws<ArgumentException>(() =>
            _incomeCategoryService.CreateIncomeCategory(userName, wrongAccountName, categoryName));
    }
    
    [Test]
    public void CreateIncomeCategory_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "IncomeCategory1";

        Assert.Throws<ArgumentException>(() =>
            _incomeCategoryService.CreateIncomeCategory(userName, accountName, wrongCategoryName));
    }

    [Test]
    public void DeleteIncomeCategory_WithCorrectData_DeletesExpenseCategoryAndChangesCategoryForTheStatementsToDefault()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "IncomeCategory1";
        var statement = IncomeStatements.GetAll()
            .FirstOrDefault(s => s.IncomeCategory.Name.Equals(categoryName));
        
        var isDeleted = _incomeCategoryService.DeleteIncomeCategory(userName, accountName, categoryName);
        
        var category = IncomeCategories.GetAll().Where(c => c.Name.Equals(categoryName)
                                                             && c.Account.User.Name.Equals(userName)
                                                             && c.Account.Name.Equals(accountName)).ToList();
        
        Assert.Multiple(() =>
        {
            Assert.True(isDeleted);
            Assert.That(statement!.IncomeCategory.Name, Is.EqualTo(IUnitOfWork.DefaultCategory));
            Assert.True(category.Count == 0);
        });
    }
    
    [Test]
    public void DeleteIncomeCategory_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "IncomeCategory1";

        Assert.Throws<ArgumentException>(() =>
            _incomeCategoryService.DeleteIncomeCategory(wrongUserName, accountName, categoryName));
    }
    
    [Test]
    public void DeleteIncomeCategory_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "IncomeCategory1";

        Assert.Throws<ArgumentException>(() =>
            _incomeCategoryService.DeleteIncomeCategory(userName, wrongAccountName, categoryName));
    }
    
    [Test]
    public void DeleteIncomeCategory_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "IncomeCategory10";

        Assert.Throws<ArgumentException>(() =>
            _incomeCategoryService.DeleteIncomeCategory(userName, accountName, wrongCategoryName));
    }
    
    [Test]
    public void DeleteIncomeCategory_WithDefaultCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var defaultCategoryName = IUnitOfWork.DefaultCategory;

        Assert.Throws<ArgumentException>(() =>
            _incomeCategoryService.DeleteIncomeCategory(userName, accountName, defaultCategoryName));
    }
    
    [Test]
    public void DeleteIncomeCategory_WithAccountWithoutDefaultCategory_TrowsArgumentException()
    {
        var userName = "user2";
        var accountName = "Account3";
        var categoryName = "IncomeCategory2";

        Assert.Throws<NoDefaultCategoryException>(() =>
            _incomeCategoryService.DeleteIncomeCategory(userName, accountName, categoryName));
    }
    
    [Test]
    public void GetAccountIncomeCategories_WithCorrectData_ReturnsExpenseCategories()
    {
        var userName = "user1";
        var accountName = "Account1";
        var expectedCategories = IncomeCategories.GetAll()
            .Where(s => s.Account.User.Name.Equals(userName)
                        && s.Account.Name.Equals(accountName));
        var expectedBusinessCategories = new List<BusinessIncomeCategory>();
        foreach (var expectedCategory in expectedCategories)
        {
            var businessIncomeCategory = new BusinessIncomeCategory()
            {
                AccountId = expectedCategory.AccountId,
                AccountName = expectedCategory.Account.Name,
                Id = expectedCategory.Id,
                Name = expectedCategory.Name
            };
            expectedBusinessCategories.Add(businessIncomeCategory);
        }

        var statements = _incomeCategoryService.GetAccountIncomeCategories(userName, accountName);
        
        Assert.That(statements, Is.EqualTo(expectedBusinessCategories));
    }
    
    [Test]
    public void GetAccountIncomeCategories_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";

        Assert.Throws<ArgumentException>(() => _incomeCategoryService.GetAccountIncomeCategories(wrongUserName, accountName));
    }
    
    [Test]
    public void GetAccountIncomeCategories_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";

        Assert.Throws<ArgumentException>(() => _incomeCategoryService.GetAccountIncomeCategories(userName, wrongAccountName));
    }
    
    [Test]
    public void GetDefaultCategoryName_ReturnsDefaultCategoryName()
    {
        var defaultCategory = _incomeCategoryService.GetDefaultCategoryName();
        
        Assert.That(defaultCategory, Is.EqualTo(IUnitOfWork.DefaultCategory));
    }
}