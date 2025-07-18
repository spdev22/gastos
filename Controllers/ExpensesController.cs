using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenses.Entities;
using PersonalExpenses.Services;
using PersonalExpenses.ViewModels;

namespace PersonalExpenses.Controllers;

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
    // GET: Expenses
    public async Task<IActionResult> Index()
    {
        var expenses = await _expenseService.GetAllAsync();
        return View(expenses);
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

        var expense = new Expense()
        {
            Amount = createExpenseModel.Expense,
            Description = createExpenseModel.Description,
            Currency = createExpenseModel.Currency,
            CategoryId = createExpenseModel.CategoryId,
            Date = createExpenseModel.Date
        };
        await _expenseService.CreateAsync(expense);

        return RedirectToAction(nameof(Index));
    }

    // POST: Expenses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _expenseService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // GET: Expenses/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var expense = await _expenseService.GetByIdAsync(id);
        if (expense == null) return NotFound();
        var categories = await _categoryService.GetAllAsync();

        var editExpenseModel = new EditExpenseModel()
        {
            Categories = categories.ToList(),
            Description = expense.Description,
            Currency = expense.Currency,
            Date = expense.Date,
            CategoryId = expense.CategoryId,
            Expense = expense.Amount,
            Id = expense.Id
        };
        return View(editExpenseModel);
    }

    // POST: Expenses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditExpenseModel editExpenseModel)
    {
        if (id != editExpenseModel.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(editExpenseModel);
        }
        var expense = new Expense()
        {
            Amount = editExpenseModel.Expense,
            Description = editExpenseModel.Description,
            Currency = editExpenseModel.Currency,
            CategoryId = editExpenseModel.CategoryId,
            Date = editExpenseModel.Date
        };

        await _expenseService.UpdateAsync(expense);
        return RedirectToAction(nameof(Index));
    }


    // GET: Expenses/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var expense = await _expenseService.GetByIdAsync(id);
        if (expense == null) return NotFound();
        var detailsModel = new ExpenseDetailsModel()
        {
            Id = expense.Id,
            Expense = expense.Amount,
            Date = expense.Date,
            CategoryName = expense.Category?.Name ?? string.Empty
        };
        return View(detailsModel);
    }


}
