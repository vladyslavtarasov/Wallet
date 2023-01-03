using System.Net;
using BLL.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using WalletAPI.ViewModels.AccountViewModels;
using WalletAPI.ViewModels.ExpenseStatementViewModels;

namespace WalletAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("/Account")]
    public IActionResult CreateAccount(CreateAccountViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName))
            return BadRequest("Empty fields");
        
        if (model.Balance < decimal.Zero)
            return BadRequest("Wrong balance");

        try
        {
            _accountService.CreateAccount(model.UserName, model.Balance, model.AccountName);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        //return Ok(model);
        return StatusCode(StatusCodes.Status201Created, model);
    }
    
    [HttpDelete("/Account")]
    public IActionResult DeleteAccount(DeleteAccountViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName))
            return BadRequest("Empty fields");

        try
        {
            _accountService.DeleteAccount(model.UserName, model.AccountName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }

        return NoContent();
    }
    
    [HttpGet("/Account")]
    public IActionResult GetAccounts(string userName)
    {
        List<BusinessAccount> accounts;
        
        if (string.IsNullOrEmpty(userName))
            return BadRequest("Empty field");

        try
        {
            accounts = _accountService.GetUserAccounts(userName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }
        return !accounts.Any() 
            ? Ok("No accounts for this user") 
            : Ok(accounts.Select(Mapper.CreateAccountViewModel).ToList());
    }
    
    [HttpPost("/Account/TransferBalance")]
    public IActionResult TransferBalance(TransferBalanceViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.FromAccountName) 
            || string.IsNullOrEmpty(model.ToAccountName))
            return BadRequest("Empty fields");
        
        if (model.Amount <= decimal.Zero)
            return BadRequest("Wrong balance");

        try
        {
            _accountService.TransferBalance(model.UserName, model.FromAccountName, model.ToAccountName, model.Amount);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok(model);
    }
    
    [HttpGet("/Account/SpentAmount")]
    public IActionResult SpentAmount(string userName, string accountName, string? categoryName)
    {
        decimal amount;
        
        if (string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(accountName))
            return BadRequest("Empty fields");

        try
        {
            amount = string.IsNullOrEmpty(categoryName) 
                ? _accountService.TotalSpentAmount(userName, accountName) 
                : _accountService.SpentOnCategoryAmount(userName, accountName, categoryName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }
        
        return Ok(amount);
    }
    
    [HttpGet("/Account/ReceivedAmount")]
    public IActionResult ReceivedAmount(string userName, string accountName, string? categoryName)
    {
        decimal amount;
        
        if (string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(accountName))
            return BadRequest("Empty fields");

        try
        {
            amount = string.IsNullOrEmpty(categoryName) 
                ? _accountService.TotalReceivedAmount(userName, accountName) 
                : _accountService.ReceivedOnCategoryAmount(userName, accountName, categoryName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }
        
        return Ok(amount);
    }
    
    [HttpGet("/Account/SpentStatements")]
    public IActionResult SpentStatements(string userName, string accountName, string? categoryName)
    {
        List<BusinessExpenseStatement> statements;
        
        if (string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(accountName))
            return BadRequest("Empty fields");

        try
        {
            statements = string.IsNullOrEmpty(categoryName) 
                ? _accountService.TotalSpentStatements(userName, accountName) 
                : _accountService.SpentOnCategoryStatements(userName, accountName, categoryName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }
        
        return !statements.Any() 
            ? Ok("No expense statements for this account") 
            : Ok(statements.Select(Mapper.CreateExpenseStatementViewModel).ToList());
    }
    
    [HttpGet("/Account/ReceiveStatements")]
    public IActionResult ReceiveStatements(string userName, string accountName, string? categoryName)
    {
        List<BusinessIncomeStatement> statements;
        
        if (string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(accountName))
            return BadRequest("Empty fields");

        try
        {
            statements = string.IsNullOrEmpty(categoryName) 
                ? _accountService.TotalReceivedStatements(userName, accountName) 
                : _accountService.ReceivedOnCategoryStatements(userName, accountName, categoryName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }
        
        return !statements.Any() 
            ? Ok("No income statements for this account") 
            : Ok(statements.Select(Mapper.CreateIncomeStatementViewModel).ToList());
    }
}