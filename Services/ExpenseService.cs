
using Microsoft.EntityFrameworkCore;
using PersonalExpenses.Entities;
using PersonalExpenses.Helpers;

namespace PersonalExpenses.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly PersonalExpensesDbContext _context;

        public ExpenseService(PersonalExpensesDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Expense expense)

        // Add the expense to the context and save changes

        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // try to find the expense with the given id
            var expense = await _context.Expenses.FindAsync(id);

            // if the expense is found, remove it and save the changes
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses.Include(e => e.Category).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllByUserAsync(string userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetForUserAsyncWith(string userId, List<IExpenseFilter> filters)
        {
            IQueryable<Expense> query = _context.Expenses.Include(e => e.Category)
                .Where(e => e.UserId == userId);
            foreach (var filter in filters)
            {
                query = filter.Apply(query);
            }

            var expenses = await query.ToListAsync();

            return expenses;
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }

        public Task<decimal> GetTotalForCurrentMonthAsync(string userId)
        {
            return _context.Expenses
                .Where(e => e.UserId == userId && e.Date.Month == DateTime.Now.Month && e.Date.Year == DateTime.Now.Year)
                .SumAsync(e => e.Amount);
        }
    }
}