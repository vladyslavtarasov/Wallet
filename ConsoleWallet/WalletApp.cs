using BLL.BusinessModels;
using ConsoleWallet.Controllers;

namespace ConsoleWallet;

public class WalletApp
{
    private readonly AccountController _account;
    private readonly ExpenseCategoryController _expenseCategory;
    private readonly ExpenseStatementController _expenseStatement;
    private readonly IncomeCategoryController _incomeCategory;
    private readonly IncomeStatementController _incomeStatement;
    private readonly UserController _user;
    
    private string _userName;
    private string _accountName;
    
    public WalletApp(AccountController account, ExpenseCategoryController expenseCategory,
        ExpenseStatementController expenseStatement, IncomeCategoryController incomeCategory,
        IncomeStatementController incomeStatement, UserController user)
    {
        _account = account;
        _expenseCategory = expenseCategory;
        _expenseStatement = expenseStatement;
        _incomeCategory = incomeCategory;
        _incomeStatement = incomeStatement;
        _user = user;
    }

    public void StartWallet()
    {
        Console.Clear();
        int choice;
        do
        {
            choice = MainMenu();
            Console.ReadKey(true);
            //Console.Clear();
        } while (choice != 0);
    }

    private int MainMenu()
    {
        Console.Clear();
        int choice;

        if (_userName is null)
        {
            Console.WriteLine("1 - Register");
            Console.WriteLine("2 - Login");
            Console.WriteLine("0 - Exit");
            Console.WriteLine();
            
            choice = UserChoice.GetChoice(0, 2);

            switch (choice)
            {
                case 1:
                    _userName = _user.Register();
                    break;
                case 2:
                    _userName = _user.Login();
                    break;
            }
        }
        else
        {
            Console.WriteLine($"User - {_userName}");
            Console.WriteLine("1 - Create an account");
            Console.WriteLine("2 - Choose an account");
            Console.WriteLine("3 - Logout");
            Console.WriteLine("0 - Exit");
            Console.WriteLine();
            
            choice = UserChoice.GetChoice(0, 3);

            int accountChoice, categoryChoice;
            switch (choice)
            {
                case 1:
                    _accountName = _account.CreateAccount(_userName);
                    if (_accountName is not null)
                    {
                        do
                        {
                            accountChoice = AccountMenu();
                            Console.ReadKey(true);
                            //Console.Clear();
                        } while (accountChoice != 0 && accountChoice != 7);
                    }
                    break;
                case 2:
                    _accountName = _account.ChooseAccount(_userName);
                    if (_accountName is not null)
                    {
                        do
                        {
                            accountChoice = AccountMenu();
                            Console.ReadKey(true);
                            //Console.Clear();
                        } while (accountChoice != 0 && accountChoice != 7);
                    }
                    break;
                case 3:
                    _userName = _user.Logout();
                    break;
            }
        }

        return choice;
    }

    private int AccountMenu()
    {
        Console.Clear();
        Console.WriteLine($"User - {_userName}");
        Console.WriteLine($"Account - {_accountName}");
        Console.WriteLine($"Balance - {_account.GetBalance(_userName, _accountName)}");
        
        Console.WriteLine("1 - Create an expense statement");
        Console.WriteLine("2 - Create an income statement");
        Console.WriteLine("3 - Create or delete an expense category");
        Console.WriteLine("4 - Create or delete an income category");
        Console.WriteLine("5 - Transfer balance to your another account");
        Console.WriteLine("6 - Payment history");
        Console.WriteLine("7 - Delete account");
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
        
        var choice = UserChoice.GetChoice(0, 7);
        string category;
        int categoryChoice;
        
        switch (choice)
        {
            case 1:
                category = _expenseCategory.ChooseExpenseCategory(_userName, _accountName);
                if (category is not null)
                {
                    _expenseStatement.CreateExpenseStatement(_userName, _accountName, category);
                }
                break;
            case 2:
                category = _incomeCategory.ChooseIncomeCategory(_userName, _accountName);
                if (category is not null)
                {
                    _incomeStatement.CreateIncomeStatement(_userName, _accountName, category);
                }
                break;
            case 3:
                do
                {
                    categoryChoice = ExpenseCategoryMenu();
                    Console.ReadKey(true);
                    //Console.Clear();
                } while (categoryChoice != 0);
                break;
            case 4:
                do
                {
                    categoryChoice = IncomeCategoryMenu();
                    Console.ReadKey(true);
                    //Console.Clear();
                } while (categoryChoice != 0);
                break;
            case 5:
                _account.TransferBalance(_userName, _accountName);
                break;
            case 6:
                int historyChoice;
                do
                {
                    historyChoice = HistoryMenu();
                    Console.ReadKey(true);
                    //Console.Clear();
                } while (historyChoice != 0);
                break;
            case 7:
                _account.DeleteAccount(_userName, _accountName);
                break;
        }

        return choice;
    }

    private int ExpenseCategoryMenu()
    {
        Console.Clear();
        Console.WriteLine($"User - {_userName}");
        Console.WriteLine($"Account - {_accountName}");

        Console.WriteLine("1 - Create an expense category");
        Console.WriteLine("2 - Delete an expense category");
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
        
        var choice = UserChoice.GetChoice(0, 2);
        string category;
        
        switch (choice)
        {
            case 1:
                _expenseCategory.CreateExpenseCategory(_userName, _accountName);
                break;
            case 2:
                category = _expenseCategory.ChooseExpenseCategoryToDelete(_userName, _accountName);
                if (category is not null)
                {
                    _expenseCategory.DeleteExpenseCategory(_userName, _accountName, category);
                }
                break;
        }

        return choice;
    }
    
    private int IncomeCategoryMenu()
    {
        Console.Clear();
        Console.WriteLine($"User - {_userName}");
        Console.WriteLine($"Account - {_accountName}");

        Console.WriteLine("1 - Create an income category");
        Console.WriteLine("2 - Delete an income category");
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
        
        var choice = UserChoice.GetChoice(0, 2);
        string category;
        
        switch (choice)
        {
            case 1:
                _incomeCategory.CreateIncomeCategory(_userName, _accountName);
                break;
            case 2:
                category = _incomeCategory.ChooseIncomeCategoryToDelete(_userName, _accountName);
                if (category is not null)
                {
                    _incomeCategory.DeleteIncomeCategory(_userName, _accountName, category);
                }
                break;
        }

        return choice;
    }

    private int HistoryMenu()
    {
        Console.Clear();
        Console.WriteLine($"User - {_userName}");
        Console.WriteLine($"Account - {_accountName}");

        Console.WriteLine("1 - Total spent amount");
        Console.WriteLine("2 - Spent amount on a specific category");
        Console.WriteLine("3 - Total income amount");
        Console.WriteLine("4 - Income amount on a specific category");
        Console.WriteLine("5 - Spent statements");
        Console.WriteLine("6 - Spent statements on a specific category");
        Console.WriteLine("7 - Income statements");
        Console.WriteLine("8 - Income statements on a specific category");
        
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
        
        var choice = UserChoice.GetChoice(0, 8);
        string category;
        
        switch (choice)
        {
            case 1:
                _account.TotalSpentAmount(_userName, _accountName);
                break;
            case 2:
                category = _expenseCategory.ChooseExpenseCategory(_userName, _accountName);
                if (category is not null)
                {
                    _account.SpentOnCategoryAmount(_userName, _accountName, category);
                }
                break;
            case 3:
                _account.TotalReceivedAmount(_userName, _accountName);
                break;
            case 4:
                category = _incomeCategory.ChooseIncomeCategory(_userName, _accountName);
                if (category is not null)
                {
                    _account.ReceivedOnCategoryAmount(_userName, _accountName, category);
                }
                break;
            case 5:
                _account.TotalSpentStatements(_userName, _accountName);
                break;
            case 6:
                category = _expenseCategory.ChooseExpenseCategory(_userName, _accountName);
                if (category is not null)
                {
                    _account.SpentOnCategoryStatements(_userName, _accountName, category);
                }
                break;
            case 7:
                _account.TotalReceivedStatements(_userName, _accountName);
                break;
            case 8:
                category = _incomeCategory.ChooseIncomeCategory(_userName, _accountName);
                if (category is not null)
                {
                    _account.ReceivedOnCategoryStatements(_userName, _accountName, category);
                }
                break;
        }
        
        return choice;
    }
}