using BLL.Services.Interfaces;
using DAL;
using DAL.Models;

namespace BLL.Services.Realizations;

public class IncomeStatementService : IIncomeStatementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ModelsFinder _modelsFinder;

    public IncomeStatementService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _modelsFinder = new ModelsFinder(_unitOfWork);
    }

    public bool CreateIncomeStatement(string userName, string accountName, string categoryName, decimal amount)
    {
        _modelsFinder.GetUser(userName);

        var category = _modelsFinder.GetIncomeCategory(userName, accountName, categoryName);

        var account = _modelsFinder.GetAccount(userName, accountName);
        
        var statement = new IncomeStatement
        {
            IncomeCategory = category,
            Amount = amount,
            DateTime = DateTime.Now
        };
        
        account.Balance += amount;
        _unitOfWork.IncomeStatements.Create(statement);
        _unitOfWork.Save();

        return true;
    }
}