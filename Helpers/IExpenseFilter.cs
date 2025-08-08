using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalExpenses.Entities;

namespace PersonalExpenses.Helpers
{
    public interface IExpenseFilter
    {
        IQueryable<Expense> Apply(IQueryable<Expense> query);
    }
}