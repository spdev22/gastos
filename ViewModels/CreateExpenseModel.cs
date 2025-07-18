using PersonalExpenses.Entities;

namespace PersonalExpenses.ViewModels
{
    public class CreateExpenseModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public decimal Expense { get; set; } = decimal.MinValue;
        public string Description { get; set; } = string.Empty;
        public Currency Currency { get; set; } = Currency.ARS;
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
    }
}