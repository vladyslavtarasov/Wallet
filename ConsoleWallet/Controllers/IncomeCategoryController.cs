using BLL.BusinessModels;
using BLL.Services.Interfaces;

namespace ConsoleWallet.Controllers;

public class IncomeCategoryController
{
    private IIncomeCategoryService _incomeCategoryService;

    public IncomeCategoryController(IIncomeCategoryService incomeCategoryService)
    {
        _incomeCategoryService = incomeCategoryService;
    }
    
    public void CreateIncomeCategory(string userName, string accountName)
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
            _incomeCategoryService.CreateIncomeCategory(userName, accountName, categoryName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }
    }
    
    public void DeleteIncomeCategory(string userName, string accountName, string categoryName)
    {
        try
        {
            _incomeCategoryService.DeleteIncomeCategory(userName, accountName, categoryName);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public string ChooseIncomeCategory(string userName, string accountName)
    {
        int choice;
        string categoryName;
        List<BusinessIncomeCategory> businessIncomeCategories;
        
        Console.WriteLine($"User - {userName}");
        Console.WriteLine($"Account - {accountName}");
        Console.WriteLine("Choose income category:");
        businessIncomeCategories = _incomeCategoryService.GetAccountIncomeCategories(userName, accountName);
        PrintIncomeCategories(businessIncomeCategories);
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
        
        choice = UserChoice.GetChoice(0, businessIncomeCategories.Count);
        if (choice == 0)
            return null;

        categoryName = businessIncomeCategories[choice - 1].Name;
        return categoryName;
    }
    
    public string ChooseIncomeCategoryToDelete(string userName, string accountName)
    {
        int choice;
        string categoryName;
        List<BusinessIncomeCategory> businessIncomeCategories;
        
        Console.WriteLine($"User - {userName}");
        Console.WriteLine($"Account - {accountName}");
        businessIncomeCategories = _incomeCategoryService.GetAccountIncomeCategories(userName, accountName);
        
        var defaultCategory =
            businessIncomeCategories.FirstOrDefault(c =>
                c.Name.Equals(_incomeCategoryService.GetDefaultCategoryName()));
        if (defaultCategory is not null)
            businessIncomeCategories.Remove(defaultCategory);
        
        if (businessIncomeCategories.Count != 0)
            PrintIncomeCategories(businessIncomeCategories);
        else
            Console.WriteLine("You do not have any income categories to delete");
        
        Console.WriteLine("0 - Go back");
        Console.WriteLine();
        
        choice = UserChoice.GetChoice(0, businessIncomeCategories.Count);
        if (choice == 0)
            return null;

        categoryName = businessIncomeCategories[choice - 1].Name;
        return categoryName;
    }
    
    public void PrintIncomeCategories(ICollection<BusinessIncomeCategory> incomeCategories)
    {
        if (incomeCategories.Count == 0)
        {
            Console.WriteLine("You do not have any income categories");
            return;
        }
        var categoryNumber = 1;
        foreach (var category in incomeCategories)
        {
            Console.Write($"{categoryNumber++} -> ");
            Console.WriteLine(category);
            Console.WriteLine();
        }
    }
}