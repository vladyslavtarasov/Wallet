using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Realizations;

public class IncomeStatementRepository : IRepository<IncomeStatement>
{
    private WalletContext _db;

    public IncomeStatementRepository(WalletContext db)
    {
        _db = db;
    }

    public IEnumerable<IncomeStatement> GetAll()
    {
        return _db.IncomeStatements
            .Include(s => s.IncomeCategory);
    }

    public IncomeStatement Get(int id)
    {
        return _db.IncomeStatements.Find(id);
    }

    public void Create(IncomeStatement item)
    {
        _db.IncomeStatements.Add(item);
    }

    public void Update(IncomeStatement item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        IncomeStatement statement = _db.IncomeStatements.Find(id);
        if (statement is not null)
            _db.IncomeStatements.Remove(statement);
    }
}