using BLL.Services.Interfaces;
using DAL;
using DAL.Models;

namespace BLL.Services.Realizations;

public class ExpenseStatementService : IExpenseStatementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ModelsFinder _modelsFinder;

    public ExpenseStatementService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _modelsFinder = new ModelsFinder(_unitOfWork);
    }

    public bool CreateExpenseStatement(string userName, string accountName, string categoryName, decimal amount)
    {
        _modelsFinder.GetUser(userName);

        var category = _modelsFinder.GetExpenseCategory(userName, accountName, categoryName);

        var account = _modelsFinder.GetAccount(userName, accountName);

        var statement = new ExpenseStatement
        {
            ExpenseCategory = category,
            Amount = amount,
            DateTime = DateTime.Now
        };

        account.Balance -= amount;
        _unitOfWork.ExpenseStatements.Create(statement);
        _unitOfWork.Save();

        return true;
    }
}