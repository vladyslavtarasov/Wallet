namespace WalletServicesTests;

public static class ModelsMockCreator
{
    private static User _user1 = new()
        { Id = 1, Name = "user1", Surname = "user1", UserName = "user1", Password = "user1" };
    private static User _user2 = new()
        { Id = 2, Name = "user2", Surname = "user2", UserName = "user2", Password = "user2" };

    private static Account _account1 = new()
        { Id = 1, Balance = 1000m, Name = "Account1", UserId = 1, User = _user1 };
    private static Account _account2 = new()
        { Id = 2, Balance = 1000m, Name = "Account2", UserId = 1, User = _user1 };
    private static Account _account3 = new()
        { Id = 3, Balance = 1000m, Name = "Account3", UserId = 2, User = _user2 };
    private static Account _accountToDelete = new()
        { Id = 4, Balance = 1000m, Name = "AccountToDelete", UserId = 1, User = _user1 };

    private static ExpenseCategory _expenseCategory1 = new()
        { Id = 1, Name = "ExpenseCategory1", AccountId = 1, Account = _account1 };
    private static ExpenseCategory _expenseCategory2 = new()
        { Id = 2, Name = "ExpenseCategory2", AccountId = 3, Account = _account3 };
    private static ExpenseCategory _defaultExpenseCategory = new()
        { Id = 4, Name = IUnitOfWork.DefaultCategory, AccountId = 1, Account = _account1 };
    
    private static IncomeCategory _incomeCategory1 = new()
        { Id = 1, Name = "IncomeCategory1", AccountId = 1, Account = _account1 };
    private static IncomeCategory _incomeCategory2 = new()
        { Id = 2, Name = "IncomeCategory2", AccountId = 3, Account = _account3 };
    private static IncomeCategory _defaultIncomeCategory = new()
        { Id = 4, Name = IUnitOfWork.DefaultCategory, AccountId = 1, Account = _account1 };

    private static ExpenseStatement _expenseStatement1 = new()
        { Id = 1, Amount = 100m, DateTime = DateTime.Now, ExpenseCategoryId = 1, ExpenseCategory = _expenseCategory1 };
    private static ExpenseStatement _expenseStatement2 = new()
        { Id = 2, Amount = 100m, DateTime = DateTime.Now, ExpenseCategoryId = 2, ExpenseCategory = _expenseCategory2 };
    
    private static IncomeStatement _incomeStatement1 = new()
        { Id = 1, Amount = 100m, DateTime = DateTime.Now, IncomeCategoryId = 1, IncomeCategory = _incomeCategory1 };
    private static IncomeStatement _incomeStatement2 = new()
        { Id = 2, Amount = 100m, DateTime = DateTime.Now, IncomeCategoryId = 2, IncomeCategory = _incomeCategory2 };
    
    private static User _userWithoutAccountsAndCategories = new()
    {
        Id = 3, Name = "UserWithoutAccountsAndCategories", Surname = "UserWithoutAccountsAndCategories",
        UserName = "UserWithoutAccountsAndCategories", Password = "UserWithoutAccountsAndCategories"
    };

    public static List<User> GetUsers()
    {
        _user1.Accounts = new List<Account> { _account1, _account2 };
        _user1.ExpenseCategories = new List<ExpenseCategory> { _expenseCategory1 };
        _user1.IncomeCategories = new List<IncomeCategory> { _incomeCategory1 };
        
        _user2.Accounts = new List<Account> { _account3 };
        _user2.ExpenseCategories = new List<ExpenseCategory> { _expenseCategory2 };
        _user2.IncomeCategories = new List<IncomeCategory> { _incomeCategory2 };

        return new List<User> { _user1, _user2, _userWithoutAccountsAndCategories };
    }

    public static List<Account> GetAccount()
    {
        _account1.ExpenseStatements = new List<ExpenseStatement> { _expenseStatement1 };
        _account1.IncomeStatements = new List<IncomeStatement> { _incomeStatement1 };
        
        _account3.ExpenseStatements = new List<ExpenseStatement> { _expenseStatement2 };
        _account3.IncomeStatements = new List<IncomeStatement> { _incomeStatement2 };

        return new List<Account> { _account1, _account2, _account3, _accountToDelete };
    }

    public static List<ExpenseCategory> GetExpenseCategories()
    {
        _expenseCategory1.ExpenseStatements = new List<ExpenseStatement> { _expenseStatement1 };
        
        _expenseCategory2.ExpenseStatements = new List<ExpenseStatement> { _expenseStatement2 };

        return new List<ExpenseCategory>
            { _expenseCategory1, _expenseCategory2, _defaultExpenseCategory };
    }

    public static List<IncomeCategory> GetIncomeCategories()
    {
        _incomeCategory1.IncomeStatements = new List<IncomeStatement> { _incomeStatement1 };

        _incomeCategory2.IncomeStatements = new List<IncomeStatement> { _incomeStatement2 };

        return new List<IncomeCategory>
            { _incomeCategory1, _incomeCategory2, _defaultIncomeCategory };
    }

    public static List<ExpenseStatement> GetExpenseStatements()
    {
        return new List<ExpenseStatement> { _expenseStatement1, _expenseStatement2 };
    }

    public static List<IncomeStatement> GetIncomeStatements()
    {
        return new List<IncomeStatement> { _incomeStatement1, _incomeStatement2 };
    }
}