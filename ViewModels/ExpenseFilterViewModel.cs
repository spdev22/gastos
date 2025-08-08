using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalExpenses.ViewModels;

public class ExpenseFilterViewModel
{
    public DateTime? FromDate { get; set; } = null;
    public DateTime? ToDate { get; set; } = null;
    public int? CategoryId { get; set; } = null;
    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
}
