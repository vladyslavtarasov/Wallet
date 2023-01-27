using BLL.BusinessModels;
using BLL.Services.Interfaces;
using DAL;
using DAL.Models;

namespace BLL.Services.Realizations;

public class IncomeCategoryService : IIncomeCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ModelsFinder _modelsFinder;

    public IncomeCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _modelsFinder = new ModelsFinder(_unitOfWork);
    }

    public bool CreateIncomeCategory(string userName, string accountName, string categoryName)
    {
        _modelsFinder.GetUser(userName);

        var account = _modelsFinder.GetAccount(userName, accountName);

        var category = _unitOfWork.IncomeCategories.GetAll()
            .FirstOrDefault(c => c.Account.User.UserName.Equals(userName) 
                                 && c.Name.Equals(categoryName)
                                 && c.Account.Name.Equals(accountName));
        if (category is not null)
            throw new ArgumentException("This category already exists", nameof(categoryName));

        category = new IncomeCategory()
        {
            Account = account,
            Name = categoryName
        };
        
        _unitOfWork.IncomeCategories.Create(category);
        _unitOfWork.Save();

        return true;
    }

    public bool DeleteIncomeCategory(string userName, string accountName, string categoryName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);

        if (categoryName.Equals(IUnitOfWork.DefaultCategory))
            throw new ArgumentException("Cannot delete default category");
        
        var category = _modelsFinder.GetIncomeCategory(userName, accountName, categoryName);
        
        var defaultCategory = _unitOfWork.IncomeCategories.GetAll()
            .FirstOrDefault(c => c.Name.Equals(IUnitOfWork.DefaultCategory) 
                                 && c.Account.Name.Equals(accountName));
        if (defaultCategory is null)
            throw new NoDefaultCategoryException("No default category for this account", nameof(userName));
        
        var statements = _unitOfWork.IncomeStatements.GetAll()
            .Where(s => s.IncomeCategory.Name.Equals(categoryName));
        
        foreach (var statement in statements)
        {
            statement.IncomeCategory = defaultCategory;
        }

        _unitOfWork.IncomeCategories.Delete(category.Id);
        _unitOfWork.Save();

        return true;
    }
    
    public List<BusinessIncomeCategory> GetAccountIncomeCategories(string userName, string accountName)
    {
        _modelsFinder.GetUser(userName);

        _modelsFinder.GetAccount(userName, accountName);

        var categories = _unitOfWork.IncomeCategories.GetAll()
            .Where(c => c.Account.User.UserName.Equals(userName) 
                        && c.Account.Name.Equals(accountName)).ToList();

        var businessCategories = new List<BusinessIncomeCategory>();
        foreach (var category in categories)
        {
            BusinessIncomeCategory incomeCategory = new BusinessIncomeCategory()
            {
                Id = category.Id,
                Name = category.Name,
                AccountId = category.AccountId,
                AccountName = category.Account.Name
            };
            businessCategories.Add(incomeCategory);
        }

        return businessCategories;
    }
    
    public string GetDefaultCategoryName()
    {
        return IUnitOfWork.DefaultCategory;
    }
    
}