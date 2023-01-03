using BLL.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using WalletAPI.ViewModels.ExpenseCategoryViewModels;

namespace WalletAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseCategoryController : ControllerBase
{
    private readonly IExpenseCategoryService _expenseCategoryService;

    public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
    {
        _expenseCategoryService = expenseCategoryService;
    }
    
    [HttpPost("/ExpenseCategory")]
    public IActionResult CreateExpenseCategory(ExpenseCategoryViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName)
            || string.IsNullOrEmpty(model.CategoryName))
            return BadRequest("Empty fields");

        try
        {
            _expenseCategoryService.CreateExpenseCategory(model.UserName, model.AccountName, model.CategoryName);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        //return Ok(model);
        return StatusCode(StatusCodes.Status201Created, model);
    }
    
    [HttpDelete("/ExpenseCategory")]
    public IActionResult DeleteExpenseCategory(ExpenseCategoryViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName))
            return BadRequest("Empty fields");

        try
        {
            _expenseCategoryService.DeleteExpenseCategory(model.UserName, model.AccountName, model.CategoryName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }

        return NoContent();
    }
    
    [HttpGet("/ExpenseCategory")]
    public IActionResult ExpenseCategories(string userName, string accountName)
    {
        List<BusinessExpenseCategory> categories;
        
        if (string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(accountName))
            return BadRequest("Empty fields");

        try
        {
            categories =  _expenseCategoryService.GetAccountExpenseCategories(userName, accountName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }
        
        return !categories.Any() 
            ? Ok("No expense categories for this account") 
            : Ok(categories.Select(Mapper.CreateExpenseCategoryViewModel).ToList());
    }
}