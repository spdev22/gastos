using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalExpenses.Entities;

namespace PersonalExpenses.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly PersonalExpensesDbContext _dbContext;

        public CategoryService(PersonalExpensesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public Task<Category?> GetByIdAsync(int id)
        {
            return _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}