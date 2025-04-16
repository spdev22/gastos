namespace PersonalExpenses.ViewModels
{
    public class ExpenseDetailsModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = "lorem ipsum";
        public decimal Expense { get; set; }
        public DateTime Date { get; set; }
        public string CategoryName { get; set; }
    }
}