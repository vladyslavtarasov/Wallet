using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using WalletAPI.ViewModels.ExpenseStatementViewModels;

namespace WalletAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseStatementController : ControllerBase
{
    private readonly IExpenseStatementService _expenseStatementService;

    public ExpenseStatementController(IExpenseStatementService expenseStatementService)
    {
        _expenseStatementService = expenseStatementService;
    }
    
    [HttpPost("/ExpenseStatement")]
    public IActionResult CreateExpenseCategory(CreateExpenseStatementViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName)
            || string.IsNullOrEmpty(model.CategoryName))
            return BadRequest("Empty fields");

        if (model.Amount <= decimal.Zero)
            return BadRequest("Wrong amount of the statement");
        
        try
        {
            _expenseStatementService.CreateExpenseStatement(model.UserName, model.AccountName, model.CategoryName, model.Amount);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        //return Ok(model);
        return StatusCode(StatusCodes.Status201Created, model);
    }
}