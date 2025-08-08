using System.Diagnostics;
using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalExpenses.Entities;
using PersonalExpenses.Helpers;
using PersonalExpenses.Services;
using PersonalExpenses.ViewModels;

namespace PersonalExpenses.Controllers;

[Authorize]
public class ExpensesController : Controller
{
    private readonly IExpenseService _expenseService;
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CreateExpenseModel> _validator;

    public ExpensesController(IExpenseService expenseService, ICategoryService categoryService, IValidator<CreateExpenseModel> validator)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
        _validator = validator;
    }

    [HttpGet]
    // GET: Expenses for the logged-in user
    public async Task<IActionResult> Index([FromQuery(Name = "Filter")] ExpenseFilterViewModel filterForm)
    {

        IEnumerable<Expense> expenses;
        filterForm.Categories = (await _categoryService.GetAllAsync())
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Set up required filters
        var filters = SetUpFilters(filterForm, userId);
        var totalThisMonth = await _expenseService.GetTotalForCurrentMonthAsync(userId!);
        expenses = filters.Count == 0 ? await _expenseService.GetAllByUserAsync(userId!) : await _expenseService.GetForUserAsyncWith(userId!, filters);

        return View(new ExpenseListViewModel()
        {
            Expenses = expenses.ToList(),
            Filter = filterForm,
            TotalThisMonth = totalThisMonth,


        });
    }

    private List<IExpenseFilter> SetUpFilters(ExpenseFilterViewModel filter, string? userId)
    {
        List<IExpenseFilter> filters = [];
        if (filter.FromDate.HasValue || filter.ToDate.HasValue)
        {
            filters.Add(new ExpenseBetweenDatesFilter(filter.FromDate, filter.ToDate));
        }
        if (filter.CategoryId.HasValue)
        {
            filters.Add(new ExpenseCategoryFilter(filter.CategoryId.Value));
        }
        return filters;
    }

    [HttpGet("Expenses/Create")]
    // GET: Expenses/Create
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllAsync();

        return View(new CreateExpenseModel()
        {
            Categories = categories.ToList(),
            Date = DateTime.Today,
            Expense = decimal.Zero
        });
    }

    [HttpPost("Expenses/Create")]
    [ValidateAntiForgeryToken]
    // POST: Expenses/Create
    public async Task<IActionResult> Create(CreateExpenseModel createExpenseModel)
    {

        var validationResult = await _validator.ValidateAsync(createExpenseModel);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            createExpenseModel.Categories = (await _categoryService.GetAllAsync()).ToList();

            return View(createExpenseModel);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId)) return BadRequest("User ID is required.");

        var expense = new Expense()
        {
            Amount = createExpenseModel.Expense,
            Description = createExpenseModel.Description,
            Currency = createExpenseModel.Currency,
            CategoryId = createExpenseModel.CategoryId,
            Date = createExpenseModel.Date,
            UserId = userId
        };
        await _expenseService.CreateAsync(expense);
        TempData["SuccessMessage"] = "Gasto agregado con éxito.";
        return RedirectToAction(nameof(Index));
    }

    // POST: Expenses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var expense = await _expenseService.GetByIdAsync(id);
        if (expense == null || expense.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier)) return NotFound();
        await _expenseService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // GET: Expenses/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var expense = await _expenseService.GetByIdAsync(id);
        var categories = await _categoryService.GetAllAsync();

        if (expense == null || expense.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier)) return RedirectToAction("Error404", "Error");

        var editExpenseModel = new EditExpenseModel()
        {
            Categories = categories.ToList(),
            Description = expense.Description,
            Currency = expense.Currency,
            Date = expense.Date,
            CategoryId = expense.CategoryId,
            Expense = expense.Amount,
        };
        return View(editExpenseModel);
    }

    // POST: Expenses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditExpenseModel editExpenseModel)
    {
        var expense = await _expenseService.GetByIdAsync(id);

        if (expense == null || expense.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier)) return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(editExpenseModel);
        }

        expense.Amount = editExpenseModel.Expense;
        expense.Description = editExpenseModel.Description;
        expense.Currency = editExpenseModel.Currency;
        expense.CategoryId = editExpenseModel.CategoryId;
        expense.Date = editExpenseModel.Date;

        await _expenseService.UpdateAsync(expense);
        return RedirectToAction(nameof(Index));
    }


    // GET: Expenses/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var expense = await _expenseService.GetByIdAsync(id);
        if (expense == null || expense.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier)) return NotFound();
        var detailsModel = new ExpenseDetailsModel()
        {
            Id = expense.Id,
            Expense = expense.Amount,
            Date = expense.Date,
            Description = expense.Description,
            CategoryName = expense.Category?.Name ?? string.Empty
        };
        return View(detailsModel);
    }

}
