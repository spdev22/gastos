
using PersonalExpenses.Entities;


namespace PersonalExpenses.ViewModels
{
    public class EditExpenseModel
    {
        public int Id { get; set; }
        public decimal Expense { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}