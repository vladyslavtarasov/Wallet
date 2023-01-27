using DAL.Repositories.Interfaces;

namespace WalletServicesTests.RepositoriesMock;

public class IncomeStatementRepositoryMock : IRepository<IncomeStatement>
{
    private List<IncomeStatement> _incomeStatements = ModelsMockCreator.GetIncomeStatements();
    public IEnumerable<IncomeStatement> GetAll()
    {
        return _incomeStatements;
    }

    public IncomeStatement Get(int id)
    {
        return _incomeStatements.FirstOrDefault(s => s.Id.Equals(id));
    }

    public void Create(IncomeStatement item)
    {
        _incomeStatements.Add(item);
    }

    public void Delete(int id)
    {
        var statement = Get(id);
        if (statement is not null)
            _incomeStatements.Remove(statement);
    }

    public void Update(IncomeStatement item) { }
}