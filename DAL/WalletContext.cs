using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class WalletContext : DbContext
{
    private string _connection;
    /*public WalletContext(string connection)
    {
        _connection = connection;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }*/
    public WalletContext(DbContextOptions<WalletContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;
    public DbSet<IncomeCategory> IncomeCategories { get; set; } = null!;
    public DbSet<ExpenseStatement> ExpenseStatements { get; set; } = null!;
    public DbSet<IncomeStatement> IncomeStatements { get; set; } = null!;
}