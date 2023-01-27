using ConsoleWallet;
using ConsoleWallet.Controllers;
using BLL;
using BLL.Services;
using BLL.Services.Interfaces;
using BLL.Services.Realizations;
using Ninject;
using DAL;
using Microsoft.EntityFrameworkCore;

const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=WalletDb;Trusted_Connection=True;";

/*
//----------------------------------------------------------
IKernel IoCWallet = new StandardKernel(new BLLConfig(connectionString));

IoCWallet.Bind<IAccountService>().To<AccountService>();
IoCWallet.Bind<IExpenseCategoryService>().To<ExpenseCategoryService>();
IoCWallet.Bind<IExpenseStatementService>().To<ExpenseStatementService>();
IoCWallet.Bind<IIncomeCategoryService>().To<IncomeCategoryService>();
IoCWallet.Bind<IIncomeStatementService>().To<IncomeStatementService>();
IoCWallet.Bind<IUserService>().To<UserService>();

IAccountService accountService = IoCWallet.Get<IAccountService>();
IExpenseCategoryService expenseCategoryService = IoCWallet.Get<IExpenseCategoryService>();
IExpenseStatementService expenseStatementService = IoCWallet.Get<IExpenseStatementService>();
IIncomeCategoryService incomeCategoryService = IoCWallet.Get<IIncomeCategoryService>();
IIncomeStatementService incomeStatementService = IoCWallet.Get<IIncomeStatementService>();
IUserService userService = IoCWallet.Get<IUserService>();

AccountController accountController = new AccountController(accountService);
ExpenseCategoryController expenseCategoryController = new ExpenseCategoryController(expenseCategoryService);
ExpenseStatementController expenseStatementController = new ExpenseStatementController(expenseStatementService);
IncomeCategoryController incomeCategoryController = new IncomeCategoryController(incomeCategoryService);
IncomeStatementController incomeStatementController = new IncomeStatementController(incomeStatementService);
UserController userController = new UserController(userService);*/

var optionsBuilder = new DbContextOptionsBuilder<WalletContext>();
var options = optionsBuilder.UseSqlServer(connectionString).Options;

using var db = new WalletContext(options);

var unitOfWork = new UnitOfWork(db);

IAccountService accountService = new AccountService(unitOfWork);
IExpenseCategoryService expenseCategoryService = new ExpenseCategoryService(unitOfWork);
IExpenseStatementService expenseStatementService = new ExpenseStatementService(unitOfWork);
IIncomeCategoryService incomeCategoryService = new IncomeCategoryService(unitOfWork);
IIncomeStatementService incomeStatementService = new IncomeStatementService(unitOfWork);
IUserService userService = new UserService(unitOfWork);

AccountController accountController = new AccountController(accountService);
ExpenseCategoryController expenseCategoryController = new ExpenseCategoryController(expenseCategoryService);
ExpenseStatementController expenseStatementController = new ExpenseStatementController(expenseStatementService);
IncomeCategoryController incomeCategoryController = new IncomeCategoryController(incomeCategoryService);
IncomeStatementController incomeStatementController = new IncomeStatementController(incomeStatementService);
UserController userController = new UserController(userService);

WalletApp walletApp = new WalletApp(accountController, expenseCategoryController,
                                    expenseStatementController, incomeCategoryController,
                                    incomeStatementController, userController);

walletApp.StartWallet();