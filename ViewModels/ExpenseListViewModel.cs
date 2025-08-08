using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalExpenses.Entities;

namespace PersonalExpenses.ViewModels
{
    public class ExpenseListViewModel
    {
        public ExpenseFilterViewModel? Filter { get; set; }
        public List<Expense>? Expenses { get; set; }
        public decimal TotalThisMonth { get; set; }
        public decimal Total { get; set; }
    }
}