using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Realizations;

public class ExpenseCategoryRepository : IRepository<ExpenseCategory>
{
    private WalletContext _db;

    public ExpenseCategoryRepository(WalletContext db)
    {
        _db = db;
    }

    public IEnumerable<ExpenseCategory> GetAll()
    {
        return _db.ExpenseCategories
            .Include(c => c.ExpenseStatements);
    }

    public ExpenseCategory Get(int id)
    {
        return _db.ExpenseCategories.Find(id);
    }

    public void Create(ExpenseCategory item)
    {
        _db.ExpenseCategories.Add(item);
    }

    public void Update(ExpenseCategory item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        ExpenseCategory category = _db.ExpenseCategories.Find(id);
        if (category is not null)
            _db.ExpenseCategories.Remove(category);
    }
}