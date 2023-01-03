using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace WalletServicesTests;

public class UnitOfWorkMock : IUnitOfWork
{
    public UnitOfWorkMock(IRepository<Account> accounts, IRepository<User> users,
        IRepository<ExpenseCategory> expenseCategories, IRepository<ExpenseStatement> expenseStatements,
        IRepository<IncomeCategory> incomeCategories, IRepository<IncomeStatement> incomeStatements)
    {
        Accounts = accounts;
        Users = users;
        ExpenseCategories = expenseCategories;
        ExpenseStatements = expenseStatements;
        IncomeCategories = incomeCategories;
        IncomeStatements = incomeStatements;
    }

    public IRepository<Account> Accounts { get; }
    public IRepository<User> Users { get; }
    public IRepository<ExpenseCategory> ExpenseCategories { get; }
    public IRepository<ExpenseStatement> ExpenseStatements { get; }
    public IRepository<IncomeCategory> IncomeCategories { get; }
    public IRepository<IncomeStatement> IncomeStatements { get; }

    public void Save() { }

    public void Dispose() { }
}