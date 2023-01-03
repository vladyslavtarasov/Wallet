using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Realizations;

public class ExpenseStatementRepository : IRepository<ExpenseStatement>
{
    private WalletContext _db;

    public ExpenseStatementRepository(WalletContext db)
    {
        _db = db;
    }

    public IEnumerable<ExpenseStatement> GetAll()
    {
        return _db.ExpenseStatements
            .Include(s => s.ExpenseCategory);
    }

    public ExpenseStatement Get(int id)
    {
        return _db.ExpenseStatements.Find(id);
    }

    public void Create(ExpenseStatement item)
    {
        _db.ExpenseStatements.Add(item);
    }

    public void Update(ExpenseStatement item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        ExpenseStatement statement = _db.ExpenseStatements.Find(id);
        if (statement is not null)
            _db.ExpenseStatements.Remove(statement);
    }
}