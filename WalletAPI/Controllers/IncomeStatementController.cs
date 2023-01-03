using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using WalletAPI.ViewModels.IncomeStatementViewModels;

namespace WalletAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class IncomeStatementController : ControllerBase
{
    private readonly IIncomeStatementService _incomeStatementService;

    public IncomeStatementController(IIncomeStatementService incomeStatementService)
    {
        _incomeStatementService = incomeStatementService;
    }

    [HttpPost("/IncomeStatement")]
    public IActionResult RegisterUser(CreateIncomeStatementViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName)
            || string.IsNullOrEmpty(model.CategoryName))
            return BadRequest("Empty fields");

        if (model.Amount <= decimal.Zero)
            return BadRequest("Wrong amount of the statement");
        
        try
        {
            _incomeStatementService.CreateIncomeStatement(model.UserName, model.AccountName, model.CategoryName, model.Amount);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok(model);
    }
}