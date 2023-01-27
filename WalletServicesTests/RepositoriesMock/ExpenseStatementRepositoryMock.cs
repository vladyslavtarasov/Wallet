using DAL.Repositories.Interfaces;

namespace WalletServicesTests.RepositoriesMock;

public class ExpenseStatementRepositoryMock : IRepository<ExpenseStatement>
{
    private List<ExpenseStatement> _expenseStatements = ModelsMockCreator.GetExpenseStatements();
    public IEnumerable<ExpenseStatement> GetAll()
    {
        return _expenseStatements;
    }

    public ExpenseStatement Get(int id)
    {
        return _expenseStatements.FirstOrDefault(s => s.Id.Equals(id));
    }

    public void Create(ExpenseStatement item)
    {
        _expenseStatements.Add(item);
    }
    
    public void Delete(int id)
    {
        var statement = Get(id);
        if (statement is not null)
            _expenseStatements.Remove(statement);
    }

    public void Update(ExpenseStatement item) { }
}