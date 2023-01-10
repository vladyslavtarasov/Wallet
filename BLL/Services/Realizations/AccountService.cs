using BLL.BusinessModels;
using BLL.Services.Interfaces;
using DAL;
using DAL.Models;

namespace BLL.Services.Realizations;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ModelsFinder _modelsFinder;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _modelsFinder = new ModelsFinder(_unitOfWork);
    }
    
    public bool CreateAccount(string userName, decimal balance, string accountName)
    {
        var user = _modelsFinder.GetUser(userName);

        var account = _unitOfWork.Accounts.GetAll()
            .FirstOrDefault(a => a.User.UserName.Equals(userName) && a.Name.Equals(accountName));
        if (account is not null)
            throw new ArgumentException("This account already exists", nameof(accountName));

        account = new Account
        {
            Balance = balance,
            Name = accountName,
            User = user
        };
        
        var expenseCategory = new ExpenseCategory
        {
            Name = "Default",
            Account = account
        };

        var incomeCategory = new IncomeCategory
        {
            Name = "Default",
            Account = account 
        };
        
        _unitOfWork.Accounts.Create(account);
        _unitOfWork.ExpenseCategories.Create(expenseCategory);
        _unitOfWork.IncomeCategories.Create(incomeCategory);
        _unitOfWork.Save();
        
        return true;
    }
    
    public bool DeleteAccount(string userName, string accountName)
    {
        _modelsFinder.GetUser(userName);

        var account = _modelsFinder.GetAccount(userName, accountName);
        
        _unitOfWork.Accounts.Delete(account.Id);
        _unitOfWork.Save();

        return true;
    }

    public decimal GetBalance(string userName, string accountName)
    {
        _modelsFinder.GetUser(userName);

        var account = _modelsFinder.GetAccount(userName, accountName);

        return account.Balance;
    }

    public List<BusinessAccount> GetUserAccounts(string userName)
    {
        _modelsFinder.GetUser(userName);

        var accounts = _unitOfWork.Accounts.GetAll()
            .Where(a => a.User.UserName.Equals(userName));

        return accounts.Select(account => new BusinessAccount
            { Id = account.Id, Balance = account.Balance, Name = account.Name, UserId = account.UserId, UserName = account.User.UserName}).ToList();
    }

    public void TransferBalance(string userName, string fromAccountName, string toAccountName, decimal amount)
    {
        _modelsFinder.GetUser(userName);

        var fromAccount = _modelsFinder.GetAccount(userName, fromAccountName);

        var toAccount = _modelsFinder.GetAccount(userName, toAccountName);

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;
        _unitOfWork.Save();
    }
    
    public decimal TotalSpentAmount(string userName, string accountName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);
        
        var spentAmount = _unitOfWork.ExpenseStatements.GetAll()
            .Where(s => s.ExpenseCategory.Account.User.UserName.Equals(userName) 
                        && s.ExpenseCategory.Account.Name.Equals(accountName))
            .Sum(s => s.Amount);
        
        return spentAmount;
    }
    
    public decimal SpentOnCategoryAmount(string userName, string accountName, string categoryName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);

        _modelsFinder.GetExpenseCategory(userName, accountName, categoryName);
        
        var spentAmount = _unitOfWork.ExpenseStatements.GetAll()
            .Where(s => s.ExpenseCategory.Account.User.UserName.Equals(userName))
            .Where(s => s.ExpenseCategory.Name.Equals(categoryName))
            .Sum(s => s.Amount);
        
        return spentAmount;
    }
    
    public decimal TotalReceivedAmount(string userName, string accountName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);
        
        var spentAmount = _unitOfWork.IncomeStatements.GetAll()
            .Where(s => s.IncomeCategory.Account.User.UserName.Equals(userName) 
                        && s.IncomeCategory.Account.Name.Equals(accountName))
            
            .Sum(s => s.Amount);
        
        return spentAmount;
    }
    
    public decimal ReceivedOnCategoryAmount(string userName, string accountName, string categoryName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);

        _modelsFinder.GetIncomeCategory(userName, accountName, categoryName);

        var spentAmount = _unitOfWork.IncomeStatements.GetAll()
            .Where(s => s.IncomeCategory.Account.User.UserName.Equals(userName))
            .Where(s => s.IncomeCategory.Name.Equals(categoryName))
            .Sum(s => s.Amount);
        
        return spentAmount;
    }
    
    public List<BusinessExpenseStatement> TotalSpentStatements(string userName, string accountName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);

        var statements = _unitOfWork.ExpenseStatements.GetAll()
            .Where(s => s.ExpenseCategory.Account.User.UserName.Equals(userName) 
                        && s.ExpenseCategory.Account.Name.Equals(accountName));

        var businessStatements = new List<BusinessExpenseStatement>();
        foreach (var statement in statements)
        {
            var businessStatement = new BusinessExpenseStatement
            {
                Id = statement.Id,
                Amount = statement.Amount,
                DateTime = statement.DateTime,
                ExpenseCategoryId = statement.ExpenseCategoryId,
                ExpenseCategoryName = statement.ExpenseCategory.Name
            };
            businessStatements.Add(businessStatement);
        }

        return businessStatements;
    }
    
    public List<BusinessExpenseStatement> SpentOnCategoryStatements(string userName, string accountName, 
        string categoryName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);

        _modelsFinder.GetExpenseCategory(userName, accountName, categoryName);
        
        var statements = _unitOfWork.ExpenseStatements.GetAll()
            .Where(s => s.ExpenseCategory.Account.User.UserName.Equals(userName) 
                        && s.ExpenseCategory.Name.Equals(categoryName));

        var businessStatements = new List<BusinessExpenseStatement>();
        foreach (var statement in statements)
        {
            var businessStatement = new BusinessExpenseStatement
            {
                Id = statement.Id,
                Amount = statement.Amount,
                DateTime = statement.DateTime,
                ExpenseCategoryId = statement.ExpenseCategoryId,
                ExpenseCategoryName = statement.ExpenseCategory.Name
            };
            businessStatements.Add(businessStatement);
        }

        return businessStatements;
    }
    
    public List<BusinessIncomeStatement> TotalReceivedStatements(string userName, string accountName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);
        
        var statements = _unitOfWork.IncomeStatements.GetAll()
            .Where(s => s.IncomeCategory.Account.User.UserName.Equals(userName) 
                        &&s.IncomeCategory.Account.Name.Equals(accountName));

        var businessStatements = new List<BusinessIncomeStatement>();
        foreach (var statement in statements)
        {
            var businessStatement = new BusinessIncomeStatement()
            {
                Id = statement.Id,
                Amount = statement.Amount,
                DateTime = statement.DateTime,
                IncomeCategoryId = statement.IncomeCategoryId,
                IncomeCategoryName = statement.IncomeCategory.Name
            };
            businessStatements.Add(businessStatement);
        }

        return businessStatements;
    }
    
    public List<BusinessIncomeStatement> ReceivedOnCategoryStatements(string userName, string accountName, 
        string categoryName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);

        _modelsFinder.GetIncomeCategory(userName, accountName, categoryName);
        
        var statements = _unitOfWork.IncomeStatements.GetAll()
            .Where(s => s.IncomeCategory.Account.User.UserName.Equals(userName) 
                        && s.IncomeCategory.Name.Equals(categoryName));

        var businessStatements = new List<BusinessIncomeStatement>();
        foreach (var statement in statements)
        {
            var businessStatement = new BusinessIncomeStatement()
            {
                Id = statement.Id,
                Amount = statement.Amount,
                DateTime = statement.DateTime,
                IncomeCategoryId = statement.IncomeCategoryId,
                IncomeCategoryName = statement.IncomeCategory.Name
            };
            businessStatements.Add(businessStatement);
        }

        return businessStatements;
    }
}