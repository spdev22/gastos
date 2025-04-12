using Microsoft.AspNetCore.Mvc;
using PersonalExpenses.Services;

namespace PersonalExpenses.Controllers;

public class ExpensesController : Controller
{
    private readonly IExpenseService _expenseService;
    private readonly ICategoryService _categoryService;

    public ExpensesController(IExpenseService expenseService, ICategoryService categoryService)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
    }

    // GET: Expenses
    public async Task<IActionResult> Index()
    {
        var expenses = await _expenseService.GetAllAsync();
        return View(expenses);
    }

    // GET: Expenses/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var expense = await _expenseService.GetByIdAsync(id);
        if (expense == null) return NotFound();
        return View(expense);
    }


    // GET: Expenses/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _categoryService.GetAllAsync();
        return View();
    }
    /* 
        // GET: Expenses/Create
        public async Task<IActionResult> Create()
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(expense);
            }

            await _expenseService.CreateAsync(expense);
            return RedirectToAction(nameof(Index));
        } */
    /* 
        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense == null) return NotFound();

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(expense);
        }
     */
    /* // POST: Expenses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Expense expense)
    {
        if (id != expense.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(expense);
        }

        await _expenseService.UpdateAsync(expense);
        return RedirectToAction(nameof(Index));
    }
 */
    /*     // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense == null) return NotFound();
            return View(expense);
        } */
    /* 
        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _expenseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        } */

}
