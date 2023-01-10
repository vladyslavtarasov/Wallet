using BLL.Services.Interfaces;
using BLL.Services.Realizations;
using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Realizations;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WalletContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IRepository<Account>, AccountRepository>();
builder.Services.AddScoped<IRepository<ExpenseCategory>, ExpenseCategoryRepository>();
builder.Services.AddScoped<IRepository<ExpenseStatement>, ExpenseStatementRepository>();
builder.Services.AddScoped<IRepository<IncomeCategory>, IncomeCategoryRepository>();
builder.Services.AddScoped<IRepository<IncomeStatement>, IncomeStatementRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<IExpenseStatementService, ExpenseStatementService>();
builder.Services.AddScoped<IIncomeCategoryService, IncomeCategoryService>();
builder.Services.AddScoped<IIncomeStatementService, IncomeStatementService>();
builder.Services.AddScoped<IUserService, UserService>();

/*builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy  =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});*/

/*var corsBuilder = new CorsPolicyBuilder();
corsBuilder.AllowAnyHeader();
corsBuilder.AllowAnyMethod();
corsBuilder.AllowAnyOrigin();

builder.Services.AddCors(options =>
{
    options.AddPolicy("WalletCorsPolicy", corsBuilder.Build());
});*/


var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseCors("WalletCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();