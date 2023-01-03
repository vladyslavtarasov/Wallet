using BLL.BusinessModels;

namespace BLL.Services.Interfaces;

public interface IAccountService
{
    public bool CreateAccount(string userName, decimal balance, string accountName);
    public bool DeleteAccount(string userName, string accountName);
    public decimal GetBalance(string userName, string accountName);
    public List<BusinessAccount> GetUserAccounts(string userName);
    public void TransferBalance(string userName, string fromAccountName, string toAccountName, decimal amount);
    public decimal TotalSpentAmount(string userName, string accountName);
    public decimal SpentOnCategoryAmount(string userName, string accountName, string categoryName);
    public decimal TotalReceivedAmount(string userName, string accountName);
    public decimal ReceivedOnCategoryAmount(string userName, string accountName, string categoryName);
    public List<BusinessExpenseStatement> TotalSpentStatements(string userName, string accountName);
    public List<BusinessExpenseStatement> SpentOnCategoryStatements(string userName, string accountName, string categoryName);
    public List<BusinessIncomeStatement> TotalReceivedStatements(string userName, string accountName);
    public List<BusinessIncomeStatement> ReceivedOnCategoryStatements(string userName, string accountName, string categoryName);
}