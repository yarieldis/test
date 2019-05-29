using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> opt) : base(opt)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            modelBuilder.Entity<ExpenseType>().HasData(
                new ExpenseType { Id = 1, Description = "Leisure"},
                new ExpenseType { Id = 2, Description = "Medical"},
                new ExpenseType { Id = 3, Description = "Education"},
                new ExpenseType { Id = 4, Description = "Insurance"});
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpensesTypes { get; set; }

    }
}
