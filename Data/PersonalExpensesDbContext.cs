
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalExpenses.Entities;

public class PersonalExpensesDbContext : IdentityDbContext<User>
{
    public PersonalExpensesDbContext(DbContextOptions<PersonalExpensesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

}
