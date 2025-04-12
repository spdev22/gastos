
using Microsoft.EntityFrameworkCore;
using PersonalExpenses.Entities;

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
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
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

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }
    }
}