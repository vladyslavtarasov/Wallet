using BLL.BusinessModels;
using WalletAPI.ViewModels.AccountViewModels;
using WalletAPI.ViewModels.ExpenseCategoryViewModels;
using WalletAPI.ViewModels.ExpenseStatementViewModels;
using WalletAPI.ViewModels.IncomeCategoryViewModels;
using WalletAPI.ViewModels.IncomeStatementViewModels;

namespace WalletAPI;

public static class Mapper
{
    public static GetAccountViewModel CreateAccountViewModel(BusinessAccount account)
    {
        var viewModelStatement = new GetAccountViewModel()
        {
            Id = account.Id,
            Balance = account.Balance,
            Name = account.Name,
            UserId = account.UserId,
            UserName = account.UserName
        };

        return viewModelStatement;
    }
    public static GetExpenseStatementViewModel CreateExpenseStatementViewModel(BusinessExpenseStatement statement)
    {
        var viewModelStatement = new GetExpenseStatementViewModel()
        {
            Id = statement.Id,
            Amount = statement.Amount,
            DateTime = statement.DateTime,
            CategoryId = statement.ExpenseCategoryId,
            CategoryName = statement.ExpenseCategoryName
        };

        return viewModelStatement;
    }
    public static GetIncomeStatementViewModel CreateIncomeStatementViewModel(BusinessIncomeStatement statement)
    {
        var viewModelStatement = new GetIncomeStatementViewModel()
        {
            Id = statement.Id,
            Amount = statement.Amount,
            DateTime = statement.DateTime,
            CategoryId = statement.IncomeCategoryId,
            CategoryName = statement.IncomeCategoryName
        };

        return viewModelStatement;
    }
    public static GetExpenseCategoryViewModel CreateExpenseCategoryViewModel(BusinessExpenseCategory category)
    {
        var viewModelStatement = new GetExpenseCategoryViewModel()
        {
            Id = category.Id,
            AccountId = category.AccountId,
            AccountName = category.AccountName,
            CategoryName = category.Name
        };

        return viewModelStatement;
    }
    public static GetIncomeCategoryViewModel CreateIncomeCategoryViewModel(BusinessIncomeCategory category)
    {
        var viewModelStatement = new GetIncomeCategoryViewModel()
        {
            Id = category.Id,
            AccountId = category.AccountId,
            AccountName = category.AccountName,
            CategoryName = category.Name
        };

        return viewModelStatement;
    }
}