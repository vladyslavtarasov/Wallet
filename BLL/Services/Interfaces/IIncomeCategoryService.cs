using BLL.BusinessModels;

namespace BLL.Services.Interfaces;

public interface IIncomeCategoryService
{
    public bool CreateIncomeCategory(string userName, string accountName, string categoryName);
    public bool DeleteIncomeCategory(string userName, string accountName, string categoryName);
    public List<BusinessIncomeCategory> GetAccountIncomeCategories(string userName, string accountName);
    public string GetDefaultCategoryName();
}