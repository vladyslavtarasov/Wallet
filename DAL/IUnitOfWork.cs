using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL;

public interface IUnitOfWork
{
    public const string DefaultCategory = "Default";
    public IRepository<Account> Accounts { get; }
    public IRepository<User> Users { get; }
    public IRepository<ExpenseCategory> ExpenseCategories { get; }
    public IRepository<ExpenseStatement> ExpenseStatements { get; }
    public IRepository<IncomeCategory> IncomeCategories { get; }
    public IRepository<IncomeStatement> IncomeStatements { get; }

    public void Save();
    public void Dispose();
}