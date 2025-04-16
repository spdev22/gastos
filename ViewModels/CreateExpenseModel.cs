using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PersonalExpenses.Entities;

namespace PersonalExpenses.ViewModels
{
    public class CreateExpenseModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();

        [Required(ErrorMessage = "An expense is required")]
        public decimal Expense { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

    }
}