
using PersonalExpenses.Entities;


namespace PersonalExpenses.ViewModels
{
    public class EditExpenseModel
    {
        public int Id { get; set; }
        public decimal Expense { get; set; }
        public string Description { get; set; } = string.Empty;
        public Currency Currency { get; set; } = Currency.ARS;
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}