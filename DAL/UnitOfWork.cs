using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Realizations;

namespace DAL;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private WalletContext _db;
    public IRepository<Account> Accounts { get; }
    public IRepository<User> Users { get; }
    public IRepository<ExpenseCategory> ExpenseCategories { get; }
    public IRepository<ExpenseStatement> ExpenseStatements { get; }
    public IRepository<IncomeCategory> IncomeCategories { get; }
    public IRepository<IncomeStatement> IncomeStatements { get; }
    
    /*public UnitOfWork(string connectionString)
    {
        _db = new WalletContext(connectionString);
        Accounts = new AccountRepository(_db);
        Users = new UserRepository(_db);
        ExpenseCategories = new ExpenseCategoryRepository(_db);
        ExpenseStatements = new ExpenseStatementRepository(_db);
        IncomeCategories = new IncomeCategoryRepository(_db);
        IncomeStatements = new IncomeStatementRepository(_db);
    }*/
    
    public UnitOfWork(WalletContext db)
    {
        _db = db;
        Accounts = new AccountRepository(_db);
        Users = new UserRepository(_db);
        ExpenseCategories = new ExpenseCategoryRepository(_db);
        ExpenseStatements = new ExpenseStatementRepository(_db);
        IncomeCategories = new IncomeCategoryRepository(_db);
        IncomeStatements = new IncomeStatementRepository(_db);
    }
    public void Save()
    {
        _db.SaveChanges();
    }
    
    private bool _disposed = false;
    
    public virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
                _db.Dispose();
            
            this._disposed = true;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

