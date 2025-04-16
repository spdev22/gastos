using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalExpenses.Entities;

public class Seeder
{
    private readonly PersonalExpensesDbContext _context;

    public Seeder(PersonalExpensesDbContext context)
    {
        _context = context;
    }

    public async Task SeedDbAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await SeedCategoriesAsync();
    }

    public async Task SeedCategoriesAsync()
    {
        await _context.Database.MigrateAsync();
        if (await _context.Categories.AnyAsync()) return;
        var categories = new List<Category>
        {
            new Category { Name = "Food" },
            new Category { Name = "Transport" },
            new Category { Name = "Entertainment" },
            new Category { Name = "Others" }
        };

        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
    }

    /* 

        public static void SeedCategories(PersonalExpensesDbContext context)
        {
            if (context.Categories.Any()) return;
            context.Categories.Add(new Category { Name = "Food" });
            context.Categories.Add(new Category { Name = "Transport" });
            context.Categories.Add(new Category { Name = "Entertainment" });
            context.Categories.Add(new Category { Name = "Others" });
            context.SaveChanges();
        } */
}
