using BLL.BusinessModels;
using BLL.Services.Interfaces;

namespace ConsoleWallet.Controllers;

public class ExpenseCategoryController
{
    private IExpenseCategoryService _expenseCategoryService;

    public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
    {
        _expenseCategoryService = expenseCategoryService;
    }

    public void CreateExpenseCategory(string userName, string accountName)
    {
        string categoryName;
        
        bool dataIsCorrect;
        do
        {
            dataIsCorrect = true;
            Console.WriteLine("Enter category name");
            categoryName = Console.ReadLine();
            if (categoryName?.Length is > 0 and <= 30) continue;
            dataIsCorrect = false;
            Console.WriteLine("Enter correct category name");
        } while (!dataIsCorrect);
        Console.WriteLine();

        try
        {
            _expenseCategoryService.CreateExpenseCategory(userName, accountName, categoryName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }
    }
    
    public void DeleteExpenseCategory(string userName, string accountName, string categoryName)
    {
        try
        {
            _expenseCategoryService.DeleteExpenseCategory(userName, accountName, categoryName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public string ChooseExpenseCategory(string userName, string accountName)
    {
        int choice;
        string categoryName;
        List<BusinessExpenseCategory> businessExpenseCategories;
        
        Console.WriteLine($"User - {userName}");
        Console.WriteLine($"Account - {accountName}");
        Console.WriteLine("Choose expense category:");
        businessExpenseCategories = _expenseCategoryService.GetAccountExpenseCategories(userName, accountName);
        PrintExpenseCategories(businessExpenseCategories);
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
        
        choice = UserChoice.GetChoice(0, businessExpenseCategories.Count);
        if (choice == 0)
            return null;

        categoryName = businessExpenseCategories[choice - 1].Name;
        return categoryName;
    }
    
    public string ChooseExpenseCategoryToDelete(string userName, string accountName)
    {
        int choice;
        string categoryName;
        List<BusinessExpenseCategory> businessExpenseCategories;
        
        Console.WriteLine($"User - {userName}");
        Console.WriteLine($"Account - {accountName}");
        businessExpenseCategories = _expenseCategoryService.GetAccountExpenseCategories(userName, accountName);
        var defaultCategory =
            businessExpenseCategories.FirstOrDefault(c =>
                c.Name.Equals(_expenseCategoryService.GetDefaultCategoryName()));
        if (defaultCategory is not null)
            businessExpenseCategories.Remove(defaultCategory);

        if (businessExpenseCategories.Count != 0)
            PrintExpenseCategories(businessExpenseCategories);
        else
            Console.WriteLine("You do not have any expense categories to delete");

        Console.WriteLine("0 - Go back");
        Console.WriteLine();
        
        choice = UserChoice.GetChoice(0, businessExpenseCategories.Count);
        if (choice == 0)
            return null;

        categoryName = businessExpenseCategories[choice - 1].Name;
        return categoryName;
    }
    
    public void PrintExpenseCategories(ICollection<BusinessExpenseCategory> expenseCategories)
    {
        if (expenseCategories.Count == 0)
        {
            Console.WriteLine("You do not have any expense categories");
            return;
        }
        var categoryNumber = 1;
        foreach (var category in expenseCategories)
        {
            Console.Write($"{categoryNumber++} -> ");
            Console.WriteLine(category);
            Console.WriteLine();
        }
    }
}