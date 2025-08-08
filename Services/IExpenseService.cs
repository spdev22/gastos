using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalExpenses.Entities;
using PersonalExpenses.Helpers;

namespace PersonalExpenses.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(int id);
        Task<IEnumerable<Expense>> GetForUserAsyncWith(string userId, List<IExpenseFilter> filters);
        Task<IEnumerable<Expense>> GetAllByUserAsync(string userId);
        Task CreateAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(int id);
        Task<decimal> GetTotalForCurrentMonthAsync(string userId);
    }
}