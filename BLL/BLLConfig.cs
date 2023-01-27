using DAL;
using Ninject.Modules;

namespace BLL;

public class BLLConfig : NinjectModule
{
    private string _connectionString;

    public BLLConfig(string connectionString)
    {
        _connectionString = connectionString;
    }

    public override void Load()
    {
        //Bind(typeof(UnitOfWork)).ToSelf().WithConstructorArgument(_connectionString);
        Bind(typeof(IUnitOfWork)).To<UnitOfWork>().WithConstructorArgument(_connectionString);
    }
}