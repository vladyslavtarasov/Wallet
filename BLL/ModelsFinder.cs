using DAL;
using DAL.Models;

namespace BLL;

public class ModelsFinder
{
    private IUnitOfWork _unitOfWork;
    public ModelsFinder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public User GetUser(string userName)
    {
        var user = _unitOfWork.Users.GetAll()
            .FirstOrDefault(u => u.UserName.Equals(userName));
        if (user is null)
            throw new ArgumentException("This user does not exist", nameof(userName));

        return user;
    }
    public Account GetAccount(string userName, string accountName)
    {
        var account = _unitOfWork.Accounts.GetAll()
            .FirstOrDefault(a => a.User.UserName.Equals(userName) && a.Name.Equals(accountName));
        if (account is null)
            throw new ArgumentException("This account does not exist", nameof(accountName));

        return account;
    }
    public ExpenseCategory GetExpenseCategory(string userName, string accountName, string categoryName)
    {
        var category = _unitOfWork.ExpenseCategories.GetAll()
            .FirstOrDefault(c => c.Account.User.UserName.Equals(userName) 
                                 && c.Name.Equals(categoryName)
                                 && c.Account.Name.Equals(accountName));
        if (category is null)
            throw new ArgumentException("This category does not exist", nameof(categoryName));

        return category;
    }
    public IncomeCategory GetIncomeCategory(string userName, string accountName,  string categoryName)
    {
        var category = _unitOfWork.IncomeCategories.GetAll()
            .FirstOrDefault(c => c.Account.User.UserName.Equals(userName) 
                                 && c.Name.Equals(categoryName)
                                 && c.Account.Name.Equals(accountName));
        if (category is null)
            throw new ArgumentException("This category does not exist", nameof(categoryName));

        return category;
    }
}