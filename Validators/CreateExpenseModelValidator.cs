using FluentValidation;
using PersonalExpenses.Services;
using PersonalExpenses.ViewModels;

namespace PersonalExpenses.Validators
{
    public class CreateExpenseModelValidator : AbstractValidator<CreateExpenseModel>
    {
        public CreateExpenseModelValidator(ICategoryService categoryService)
        {
            RuleFor(x => x.Expense)
            .NotNull().WithMessage("Expense is required.")
            .NotEmpty().WithMessage("Expense cannot be empty.")
            .GreaterThan(0).WithMessage("Expense must be greater than zero.");

            RuleFor(x => x.Date)
                .NotNull().WithMessage("Date is required.")
                .NotEmpty().WithMessage("Date cannot be empty.")
                .LessThanOrEqualTo(_ => DateTime.Now).WithMessage("Date cannot be in the future.");

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("CategoryId is required.")
                .NotEmpty().WithMessage("CategoryId cannot be empty.")
                .WithMessage("CategoryId is not valid.");
        }
    }
}