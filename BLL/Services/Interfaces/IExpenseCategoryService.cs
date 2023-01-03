using BLL.BusinessModels;

namespace BLL.Services.Interfaces;

public interface IExpenseCategoryService
{
    public bool CreateExpenseCategory(string userName, string accountName, string categoryName);
    public bool DeleteExpenseCategory(string userName, string accountName, string categoryName);
    public List<BusinessExpenseCategory> GetAccountExpenseCategories(string userName, string accountName);
    public string GetDefaultCategoryName();
}