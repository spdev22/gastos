using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalExpenses.Entities;

namespace PersonalExpenses.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(int id);
        Task CreateAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(int id);
    }
}