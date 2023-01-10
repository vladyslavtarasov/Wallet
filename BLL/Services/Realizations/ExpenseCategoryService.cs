using BLL.BusinessModels;
using BLL.Services.Interfaces;
using DAL;
using DAL.Models;

namespace BLL.Services.Realizations;

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ModelsFinder _modelsFinder;
    public ExpenseCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _modelsFinder = new ModelsFinder(_unitOfWork);
    }

    public bool CreateExpenseCategory(string userName, string accountName, string categoryName)
    {
        _modelsFinder.GetUser(userName);

        var account = _modelsFinder.GetAccount(userName, accountName);

        var category = _unitOfWork.ExpenseCategories.GetAll()
            .FirstOrDefault(c => c.Account.User.UserName.Equals(userName) 
                                 && c.Name.Equals(categoryName)
                                 && c.Account.Name.Equals(accountName));
        if (category is not null)
            throw new ArgumentException("This category already exists", nameof(categoryName));
        
        category = new ExpenseCategory
        {
            Account = account,
            Name = categoryName
        };
        
        _unitOfWork.ExpenseCategories.Create(category);
        _unitOfWork.Save();
        
        return true;
    }

    public bool DeleteExpenseCategory(string userName, string accountName, string categoryName)
    {
        _modelsFinder.GetUser(userName);

        var category = _modelsFinder.GetExpenseCategory(userName, accountName, categoryName);
        if (category.Name.Equals(IUnitOfWork.DefaultCategory))
            throw new ArgumentException("Cannot delete default category");
            
        var defaultCategory = _unitOfWork.ExpenseCategories.GetAll()
            .FirstOrDefault(c => c.Name.Equals(IUnitOfWork.DefaultCategory)
            && c.Account.User.UserName.Equals(userName));
        if (defaultCategory is null)
            throw new NoDefaultCategoryException("No default category for this account", nameof(accountName));
        
        var statements = _unitOfWork.ExpenseStatements.GetAll()
            .Where(s => s.ExpenseCategory.Name.Equals(categoryName));
        
        foreach (var statement in statements)
        {
            statement.ExpenseCategory = defaultCategory;
        }
        
        _unitOfWork.ExpenseCategories.Delete(category.Id);
        _unitOfWork.Save();

        return true;
    }

    public List<BusinessExpenseCategory> GetAccountExpenseCategories(string userName, string accountName)
    {
        _modelsFinder.GetUser(userName);
        _modelsFinder.GetAccount(userName, accountName);

        var categories = _unitOfWork.ExpenseCategories.GetAll()
            .Where(c => c.Account.User.UserName.Equals(userName)
                        && c.Account.Name.Equals(accountName)).ToList();

        var businessCategories = new List<BusinessExpenseCategory>();
        foreach (var category in categories)
        {
            BusinessExpenseCategory expenseCategory = new BusinessExpenseCategory
            {
                Id = category.Id,
                Name = category.Name,
                AccountId = category.AccountId,
                AccountName = category.Account.Name
            };
            businessCategories.Add(expenseCategory);
        }

        return businessCategories;
    }

    public string GetDefaultCategoryName()
    {
        return IUnitOfWork.DefaultCategory;
    }
}