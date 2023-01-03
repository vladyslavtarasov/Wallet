using BLL.BusinessModels;
using BLL.Services.Interfaces;

namespace ConsoleWallet.Controllers;

public class AccountController
{
    private IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string CreateAccount(string userName)
    {
        string accountName;
        decimal balance;
        bool dataIsCorrect;
        do
        {
            dataIsCorrect = true;
            Console.WriteLine("Enter account name");
            accountName = Console.ReadLine();
            if (accountName?.Length is > 0 and <= 30) continue;
            dataIsCorrect = false;
            Console.WriteLine("Enter correct account name");
        } while (!dataIsCorrect);

        Console.WriteLine();
        
        do
        {
            Console.WriteLine("Enter balance");
            dataIsCorrect = decimal.TryParse(Console.ReadLine(), out balance) 
                            && balance is >= 0 and <= int.MaxValue;
            
            if (dataIsCorrect) continue;
            
            Console.WriteLine("\nEnter correct balance.\n");
        } while (!dataIsCorrect);

        Console.WriteLine();

        try
        {
            _accountService.CreateAccount(userName, balance, accountName);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("This account already exists");
            Console.WriteLine();
            return null;
        }
        return accountName;
    }
    
    public void DeleteAccount(string userName, string accountName)
    {
        try
        {
            _accountService.DeleteAccount(userName, accountName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }
    }

    public void PrintAccounts(ICollection<BusinessAccount> accounts)
    {
        if (accounts.Count == 0)
        {
            Console.WriteLine("You do not have any accounts to choose");
            return;
        }
        var accountNumber = 1;
        foreach (var account in accounts)
        {
            Console.Write($"{accountNumber++} -> ");
            Console.WriteLine(account);
            Console.WriteLine();
        }
    }

    public decimal GetBalance(string userName, string accountName)
    {
        var balance = _accountService.GetBalance(userName, accountName);
        
        return balance;
    }
    
    public string ChooseAccount(string userName)
    {
        int choice;
        string accountName;
        List<BusinessAccount> businessAccounts;

        Console.WriteLine($"User - {userName}");
        businessAccounts = _accountService.GetUserAccounts(userName);
        PrintAccounts(businessAccounts);
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
            
        choice = UserChoice.GetChoice(0, businessAccounts.Count);
        if (choice == 0)
            return null;

        accountName = businessAccounts[choice - 1].Name;
        return accountName;
    }
    
    public string ChooseAccountExcept(string userName, string exceptAccountName)
    {
        int choice;
        string accountName;
        List<BusinessAccount> businessAccounts;

        Console.WriteLine($"User - {userName}");
        businessAccounts = _accountService.GetUserAccounts(userName);
        
        var exceptAccount = businessAccounts.FirstOrDefault(a => a.Name.Equals(exceptAccountName));
        if (exceptAccount is not null)
            businessAccounts.Remove(exceptAccount);
        
        PrintAccounts(businessAccounts);
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
            
        choice = UserChoice.GetChoice(0, businessAccounts.Count);
        if (choice == 0)
            return null;

        accountName = businessAccounts[choice - 1].Name;
        return accountName;
    }

    public void TransferBalance(string userName, string fromAccountName)
    {
        decimal amount;
        bool dataIsCorrect;

        var toAccountName = ChooseAccountExcept(userName, fromAccountName);
        if (toAccountName is null)
            return;
        
        do
        {
            Console.WriteLine("Enter balance");
            dataIsCorrect = decimal.TryParse(Console.ReadLine(), out amount) 
                            && amount is >= 0 and <= int.MaxValue;
            
            if (dataIsCorrect) continue;
            
            Console.WriteLine("\nEnter correct balance.\n");
        } while (!dataIsCorrect);

        Console.WriteLine();

        try
        {
            _accountService.TransferBalance(userName, fromAccountName, toAccountName, amount);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public void TotalSpentAmount(string userName, string accountName)
    {
        decimal spent = 0m;
        try
        {
            spent = _accountService.TotalSpentAmount(userName, accountName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine($"You have spent {spent}");
        Console.WriteLine();
    }

    public void SpentOnCategoryAmount(string userName, string accountName, string categoryName)
    {
        decimal spent = 0m;
        try
        {
            spent = _accountService.SpentOnCategoryAmount(userName, accountName, categoryName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine($"You have spent {spent} on {categoryName}");
        Console.WriteLine();
    }

    public void TotalReceivedAmount(string userName, string accountName)
    {
        decimal received = 0m;
        try
        {
            received = _accountService.TotalReceivedAmount(userName, accountName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine($"You have received {received}");
        Console.WriteLine();
    }

    public void ReceivedOnCategoryAmount(string userName, string accountName, string categoryName)
    {
        decimal received = 0m;
        try
        {
            received = _accountService.ReceivedOnCategoryAmount(userName, accountName, categoryName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine($"You have received {received} on {categoryName}");
        Console.WriteLine();
    }

    public void TotalSpentStatements(string userName, string accountName)
    {
        List<BusinessExpenseStatement> statements = null;
        try
        {
            statements = _accountService.TotalSpentStatements(userName, accountName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        if (statements is null || statements.Count == 0)
        {
            Console.WriteLine("You do not have statements");
            return;
        }

        foreach (var statement in statements)
        {
            Console.WriteLine(statement);
        }

        Console.WriteLine();
    }

    public void SpentOnCategoryStatements(string userName, string accountName,
        string categoryName)
    {
        List<BusinessExpenseStatement> statements = null;
        try
        {
            statements = _accountService.SpentOnCategoryStatements(userName, accountName, categoryName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        if (statements is null || statements.Count == 0)
        {
            Console.WriteLine("You do not have statements");
            return;
        }

        foreach (var statement in statements)
        {
            Console.WriteLine(statement);
        }

        Console.WriteLine();
    }

    public void TotalReceivedStatements(string userName, string accountName)
    {
        List<BusinessIncomeStatement> statements = null;
        try
        {
            statements = _accountService.TotalReceivedStatements(userName, accountName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        if (statements is null || statements.Count == 0)
        {
            Console.WriteLine("You do not have statements");
            return;
        }

        foreach (var statement in statements)
        {
            Console.WriteLine(statement);
        }

        Console.WriteLine();
    }

    public void ReceivedOnCategoryStatements(string userName, string accountName,
        string categoryName)
    {
        List<BusinessIncomeStatement> statements = null;
        try
        {
            statements = _accountService.ReceivedOnCategoryStatements(userName, accountName, categoryName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        if (statements is null || statements.Count == 0)
        {
            Console.WriteLine("You do not have statements");
            return;
        }

        foreach (var statement in statements)
        {
            Console.WriteLine(statement);
        }

        Console.WriteLine();
    }
}