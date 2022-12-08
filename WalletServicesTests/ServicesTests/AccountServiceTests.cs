namespace WalletServicesTests.ServicesTests;

[TestFixture]
public class AccountServiceTests
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

    private readonly AccountService _accountService = new(UnitOfWork);
    
    [Test]
    public void CreateAccount_WithCorrectData_CreatesAccount()
    {
        var userName = "user1";
        var balance = 100m;
        var accountName = "account";
        var isCreated = _accountService.CreateAccount(userName, balance, accountName);

        var account = Accounts.GetAll().Where(a => a.User.Name.Equals(userName)
                                                   && a.Name.Equals(accountName)).ToList();
        
        Assert.Multiple(() =>
        {
            Assert.That(isCreated, Is.True);
            Assert.True(account.Count == 1 && account[0].Name.Equals(accountName));
        });
    }
    
    [Test]
    public void CreateAccount_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var balance = 100m;
        var accountName = "account";

        Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(wrongUserName, balance, accountName));
    }
    
    [Test]
    public void CreateAccount_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var balance = 100m;
        var wrongAccountName = "Account1";

        Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(userName, balance, wrongAccountName));
    }
    
    [Test]
    public void DeleteAccount_WithCorrectData_DeletesAccount()
    {
        var userName = "user1";
        var accountToDelete = "AccountToDelete";
        var isDeleted = _accountService.DeleteAccount(userName, accountToDelete);
        
        var account = Accounts.GetAll().Where(a => a.User.Name.Equals(userName)
                                                   && a.Name.Equals(accountToDelete)).ToList();
        
        Assert.Multiple(() =>
        {
            Assert.True(isDeleted);
            Assert.True(account.Count == 0);
        });
    }
    
    [Test]
    public void DeleteAccount_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "account";

        Assert.Throws<ArgumentException>(() => _accountService.DeleteAccount(wrongUserName, accountName));
    }
    
    [Test]
    public void DeleteAccount_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "wrongAccountName";

        Assert.Throws<ArgumentException>(() => _accountService.DeleteAccount(userName, wrongAccountName));
    }
    
    [Test]
    public void GetBalance_WithCorrectData_ReturnsBalance()
    {
        var userName = "user1";
        var accountName = "Account1";
        var balance = _accountService.GetBalance(userName, accountName);
        
        Assert.That(balance, Is.EqualTo(1000m));
    }
    
    [Test]
    public void GetBalance_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "account";

        Assert.Throws<ArgumentException>(() => _accountService.GetBalance(wrongUserName, accountName));
    }
    
    [Test]
    public void GetBalance_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "wrongAccountName";

        Assert.Throws<ArgumentException>(() => _accountService.GetBalance(userName, wrongAccountName));
    }
    
    [Test]
    public void GetUserAccounts_WithCorrectData_ReturnsBalance()
    {
        var userName = "user1";
        var expectedAccounts = Accounts.GetAll().Where(a => a.User.Name.Equals(userName)).ToList();
        var expectedBusinessAccounts = new List<BusinessAccount>();
        foreach (var expectedAccount in expectedAccounts)
        {
            var businessAccount = new BusinessAccount
            {
                Balance = expectedAccount.Balance,
                Id = expectedAccount.Id,
                Name = expectedAccount.Name,
                UserId = expectedAccount.UserId
            };
            expectedBusinessAccounts.Add(businessAccount);
        }
        
        var accounts = _accountService.GetUserAccounts(userName);
        
        Assert.That(accounts, Is.EqualTo(expectedBusinessAccounts));
    }
    
    [Test]
    public void GetUserAccounts_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";

        Assert.Throws<ArgumentException>(() => _accountService.GetUserAccounts(wrongUserName));
    }
    
    [Test]
    public void TransferBalance_WithCorrectData_TransfersBalance()
    {
        var userName = "user1";
        var fromAccount = "Account1";
        var toAccount = "Account2";
        var amount = 500m;
        var newAmount1 = 500m;
        var newAmount2 = 1500m;
        
        _accountService.TransferBalance(userName, fromAccount, toAccount, amount);
        
        Assert.Multiple(() =>
        {
            Assert.True(_accountService.GetBalance(userName, fromAccount) == newAmount1);
            Assert.True(_accountService.GetBalance(userName, toAccount) == newAmount2);
        });
    }
    
    [Test]
    public void TransferBalance_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "user10";
        var fromAccount = "Account1";
        var toAccount = "Account2";
        var amount = 500m;

        Assert.Throws<ArgumentException>(() =>
            _accountService.TransferBalance(wrongUserName, fromAccount, toAccount, amount));
    }
    
    [Test]
    public void TransferBalance_WithWrongFromAccount_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongFromAccount = "Account10";
        var toAccount = "Account2";
        var amount = 500m;

        Assert.Throws<ArgumentException>(() =>
            _accountService.TransferBalance(userName, wrongFromAccount, toAccount, amount));
    }
    
    [Test]
    public void TransferBalance_WithWrongToAccount_TrowsArgumentException()
    {
        var userName = "user1";
        var fromAccount = "Account1";
        var wrongToAccount = "Account20";
        var amount = 500m;

        Assert.Throws<ArgumentException>(() =>
            _accountService.TransferBalance(userName, fromAccount, wrongToAccount, amount));
    }
    
    [Test]
    public void TotalSpentAmount_WithCorrectData_ReturnsSpentAmount()
    {
        var userName = "user1";
        var accountName = "Account1";
        var expectedAmount = 100m;
        
        var actualAmount = _accountService.TotalSpentAmount(userName, accountName);
        
        Assert.That(expectedAmount, Is.EqualTo(actualAmount));
    }
    
    [Test]
    public void TotalSpentAmount_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        
        Assert.Throws<ArgumentException>(() => _accountService.TotalSpentAmount(wrongUserName, accountName));
    }
    
    [Test]
    public void TotalSpentAmount_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        
        Assert.Throws<ArgumentException>(() => _accountService.TotalSpentAmount(userName, wrongAccountName));
    }
    
    [Test]
    public void SpentOnCategoryAmount_WithCorrectData_ReturnsSpentAmount()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "ExpenseCategory1";
        var expectedAmount = 100m;

        var actualAmount = _accountService.SpentOnCategoryAmount(userName, accountName, categoryName);
        
        Assert.That(expectedAmount, Is.EqualTo(actualAmount));
    }
    
    [Test]
    public void SpentOnCategoryAmount_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "ExpenseCategory1";

        Assert.Throws<ArgumentException>(() =>
            _accountService.SpentOnCategoryAmount(wrongUserName, accountName, categoryName));
    }
    
    [Test]
    public void SpentOnCategoryAmount_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "ExpenseCategory1";

        Assert.Throws<ArgumentException>(() =>
            _accountService.SpentOnCategoryAmount(userName, wrongAccountName, categoryName));
    }
    
    [Test]
    public void SpentOnCategoryAmount_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "ExpenseCategory10";

        Assert.Throws<ArgumentException>(() =>
            _accountService.SpentOnCategoryAmount(userName, accountName, wrongCategoryName));
    }
    
    [Test]
    public void TotalReceivedAmount_WithCorrectData_ReturnsSpentAmount()
    {
        var userName = "user1";
        var accountName = "Account1";
        var expectedAmount = 100m;
        
        var actualAmount = _accountService.TotalReceivedAmount(userName, accountName);
        
        Assert.That(actualAmount, Is.EqualTo(expectedAmount));
    }
    
    [Test]
    public void TotalReceivedAmount_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        
        Assert.Throws<ArgumentException>(() => _accountService.TotalReceivedAmount(wrongUserName, accountName));
    }
    
    [Test]
    public void TotalReceivedAmount_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        
        Assert.Throws<ArgumentException>(() => _accountService.TotalReceivedAmount(userName, wrongAccountName));
    }
    
    [Test]
    public void ReceivedOnCategoryAmount_WithCorrectData_ReturnsSpentAmount()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "IncomeCategory1";
        var expectedAmount = 100m;

        var actualAmount = _accountService.ReceivedOnCategoryAmount(userName, accountName, categoryName);
        
        Assert.That(expectedAmount, Is.EqualTo(actualAmount));
    }
    
    [Test]
    public void ReceivedOnCategoryAmount_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "IncomeCategory1";

        Assert.Throws<ArgumentException>(() =>
            _accountService.ReceivedOnCategoryAmount(wrongUserName, accountName, categoryName));
    }
    
    [Test]
    public void ReceivedOnCategoryAmount_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "IncomeCategory1";

        Assert.Throws<ArgumentException>(() =>
            _accountService.ReceivedOnCategoryAmount(userName, wrongAccountName, categoryName));
    }
    
    [Test]
    public void ReceivedOnCategoryAmount_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "IncomeCategory10";

        Assert.Throws<ArgumentException>(() =>
            _accountService.ReceivedOnCategoryAmount(userName, accountName, wrongCategoryName));
    }
    
    [Test]
    public void TotalSpentStatements_WithCorrectData_ReturnsSpentStatements()
    {
        var userName = "user1";
        var accountName = "Account1";
        var expectedStatements = ExpenseStatements.GetAll()
            .Where(s => s.ExpenseCategory.Account.User.Name.Equals(userName)
                        && s.ExpenseCategory.Account.Name.Equals(accountName));
        var expectedBusinessStatements = new List<BusinessExpenseStatement>();
        foreach (var expectedStatement in expectedStatements)
        {
            var businessExpenseStatement = new BusinessExpenseStatement()
            {
                Amount = expectedStatement.Amount,
                DateTime = expectedStatement.DateTime,
                ExpenseCategoryId = expectedStatement.ExpenseCategoryId,
                ExpenseCategoryName = expectedStatement.ExpenseCategory.Name,
                Id = expectedStatement.Id
            };
            expectedBusinessStatements.Add(businessExpenseStatement);
        }

        var statements = _accountService.TotalSpentStatements(userName, accountName);
        
        Assert.That(statements, Is.EqualTo(expectedBusinessStatements));
    }
    
    [Test]
    public void TotalSpentStatements_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";

        Assert.Throws<ArgumentException>(() => _accountService.TotalSpentStatements(wrongUserName, accountName));
    }
    
    [Test]
    public void TotalSpentStatements_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";

        Assert.Throws<ArgumentException>(() => _accountService.TotalSpentStatements(userName, wrongAccountName));
    }
    
    [Test]
    public void SpentOnCategoryStatements_WithCorrectData_ReturnsSpentStatements()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "ExpenseCategory1";
        var expectedStatements = ExpenseStatements.GetAll()
            .Where(s => s.ExpenseCategory.Account.User.Name.Equals(userName)
                        && s.ExpenseCategory.Account.Name.Equals(accountName) 
                        && s.ExpenseCategory.Name.Equals(categoryName));
        var expectedBusinessStatements = new List<BusinessExpenseStatement>();
        foreach (var expectedStatement in expectedStatements)
        {
            var businessExpenseStatement = new BusinessExpenseStatement()
            {
                Amount = expectedStatement.Amount,
                DateTime = expectedStatement.DateTime,
                ExpenseCategoryId = expectedStatement.ExpenseCategoryId,
                ExpenseCategoryName = expectedStatement.ExpenseCategory.Name,
                Id = expectedStatement.Id
            };
            expectedBusinessStatements.Add(businessExpenseStatement);
        }

        var statements = _accountService.SpentOnCategoryStatements(userName, accountName, categoryName);
        
        Assert.That(statements, Is.EqualTo(expectedBusinessStatements));
    }
    
    [Test]
    public void SpentOnCategoryStatements_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "ExpenseCategory1";

        Assert.Throws<ArgumentException>(() =>
            _accountService.SpentOnCategoryStatements(wrongUserName, accountName, categoryName));
    }
    
    [Test]
    public void SpentOnCategoryStatements_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "ExpenseCategory1";

        Assert.Throws<ArgumentException>(() => 
            _accountService.SpentOnCategoryStatements(userName, wrongAccountName, categoryName));
    }
    
    [Test]
    public void SpentOnCategoryStatements_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "ExpenseCategory10";

        Assert.Throws<ArgumentException>(() => 
            _accountService.SpentOnCategoryStatements(userName, accountName, wrongCategoryName));
    }
    
    [Test]
    public void TotalReceivedStatements_WithCorrectData_ReturnsSpentStatements()
    {
        var userName = "user1";
        var accountName = "Account1";
        var expectedStatements = IncomeStatements.GetAll()
            .Where(s => s.IncomeCategory.Account.User.Name.Equals(userName)
                        && s.IncomeCategory.Account.Name.Equals(accountName));
        var expectedBusinessStatements = new List<BusinessIncomeStatement>();
        foreach (var expectedStatement in expectedStatements)
        {
            var businessIncomeStatement = new BusinessIncomeStatement()
            {
                Amount = expectedStatement.Amount,
                DateTime = expectedStatement.DateTime,
                IncomeCategoryId = expectedStatement.IncomeCategoryId,
                IncomeCategoryName = expectedStatement.IncomeCategory.Name,
                Id = expectedStatement.Id
            };
            expectedBusinessStatements.Add(businessIncomeStatement);
        }

        var statements = _accountService.TotalReceivedStatements(userName, accountName);
        
        Assert.That(statements, Is.EqualTo(expectedBusinessStatements));
    }
    
    [Test]
    public void TotalReceivedStatements_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";

        Assert.Throws<ArgumentException>(() => _accountService.TotalReceivedStatements(wrongUserName, accountName));
    }
    
    [Test]
    public void TotalReceivedStatements_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";

        Assert.Throws<ArgumentException>(() => _accountService.TotalReceivedStatements(userName, wrongAccountName));
    }
    
    [Test]
    public void ReceivedOnCategoryStatements_WithCorrectData_ReturnsSpentStatements()
    {
        var userName = "user1";
        var accountName = "Account1";
        var categoryName = "IncomeCategory1";
        var expectedStatements = IncomeStatements.GetAll()
            .Where(s => s.IncomeCategory.Account.User.Name.Equals(userName)
                        && s.IncomeCategory.Account.Name.Equals(accountName) 
                        && s.IncomeCategory.Name.Equals(categoryName));
        var expectedBusinessStatements = new List<BusinessIncomeStatement>();
        foreach (var expectedStatement in expectedStatements)
        {
            var businessExpenseStatement = new BusinessIncomeStatement()
            {
                Amount = expectedStatement.Amount,
                DateTime = expectedStatement.DateTime,
                IncomeCategoryId = expectedStatement.IncomeCategoryId,
                IncomeCategoryName = expectedStatement.IncomeCategory.Name,
                Id = expectedStatement.Id
            };
            expectedBusinessStatements.Add(businessExpenseStatement);
        }

        var statements = _accountService.ReceivedOnCategoryStatements(userName, accountName, categoryName);
        
        Assert.That(statements, Is.EqualTo(expectedBusinessStatements));
    }
    
    [Test]
    public void ReceivedOnCategoryStatements_WithWrongUserName_TrowsArgumentException()
    {
        var wrongUserName = "userName";
        var accountName = "Account1";
        var categoryName = "IncomeCategory1";

        Assert.Throws<ArgumentException>(() =>
            _accountService.ReceivedOnCategoryStatements(wrongUserName, accountName, categoryName));
    }
    
    [Test]
    public void ReceivedOnCategoryStatements_WithWrongAccountName_TrowsArgumentException()
    {
        var userName = "user1";
        var wrongAccountName = "Account10";
        var categoryName = "IncomeCategory1";

        Assert.Throws<ArgumentException>(() => 
            _accountService.ReceivedOnCategoryStatements(userName, wrongAccountName, categoryName));
    }
    
    [Test]
    public void ReceivedOnCategoryStatements_WithWrongCategoryName_TrowsArgumentException()
    {
        var userName = "user1";
        var accountName = "Account1";
        var wrongCategoryName = "IncomeCategory10";

        Assert.Throws<ArgumentException>(() => 
            _accountService.ReceivedOnCategoryStatements(userName, accountName, wrongCategoryName));
    }
}