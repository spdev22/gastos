using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalExpenses.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public Currency Currency { get; set; } = Currency.ARS;
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}