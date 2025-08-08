using PersonalExpenses.Entities;

namespace PersonalExpenses.Helpers
{
    public class ExpenseBetweenDatesFilter : IExpenseFilter
    {
        private readonly DateTime? _fromDate;
        private readonly DateTime? _toDate;

        public ExpenseBetweenDatesFilter(DateTime? fromDate, DateTime? toDate)
        {
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public IQueryable<Expense> Apply(IQueryable<Expense> query)
        {
            if (_fromDate.HasValue && _toDate.HasValue)
            {
                return query.Where(e => e.Date >= _fromDate && e.Date <= _toDate);
            }
            else if (_fromDate.HasValue)
            {
                return query.Where(e => e.Date >= _fromDate);
            }
            else if (_toDate.HasValue)
            {
                return query.Where(e => e.Date <= _toDate);
            }
            return query;
        }
    }
}