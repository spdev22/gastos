using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalExpenses.Entities;

namespace PersonalExpenses.Helpers
{
    public class ExpenseCategoryFilter : IExpenseFilter
    {
        private readonly int _categoryId;

        public ExpenseCategoryFilter(int categoryId)
        {
            _categoryId = categoryId;
        }
        public IQueryable<Expense> Apply(IQueryable<Expense> query)
        {
            return query.Where(e => e.CategoryId == _categoryId);
        }
    }
}