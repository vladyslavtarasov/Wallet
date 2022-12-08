namespace BLL.Interfaces;

public interface IIncomeStatementService
{
    public bool CreateIncomeStatement(string userName, string accountName, string categoryName, decimal amount);
}