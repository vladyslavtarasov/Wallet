namespace BLL.Services.Interfaces;

public interface IExpenseStatementService
{
    public bool CreateExpenseStatement(string userName, string accountName, string categoryName, decimal amount);
}