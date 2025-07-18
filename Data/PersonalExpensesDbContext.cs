
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PersonalExpenses.Entities;

public class PersonalExpensesDbContext : DbContext
{
    public PersonalExpensesDbContext(DbContextOptions<PersonalExpensesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

}
