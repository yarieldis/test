using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.HttpRequests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service
{
    /// <summary>
    /// Encapsulate Expenses data layer integration for use in MVC controllers
    /// </summary>
    public class ExpenseService : ITestService<Expense>
    {

        private readonly ExpenseContext _context;

        public ExpenseService(ExpenseContext context)
        {
            _context = context;
        }

        #region ITestService implemantation

        public async Task<Expense> Save(Expense expense)
        {
            var e = _context.Add(expense);
            await _context.SaveChangesAsync();

            return e.Entity;
        }

        public async Task<List<Expense>> GetAll()
        {
            return await _context.Expenses.Include(e => e.Type)
                .OrderBy(e => e.Id)
                .ToListAsync();
        }

        public async Task<Expense> Get(int? id)
        {
            return id.HasValue 
                ? await _context.Expenses
                .Include(e => e.Type)
                .FirstOrDefaultAsync(m => m.Id == id) 
                : null;
        }

        public async Task<Expense> Update(Expense expense)
        {
            var e = _context.Update(expense);
            await _context.SaveChangesAsync();
            return e.Entity;
        }

        public async Task Remove(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }


        public  bool Exists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }

        #endregion

        #region Own Public methods

        public DbSet<ExpenseType> GetExpenseTypesDbSet()
        {
            return _context.Set<ExpenseType>();
        }

        public async Task<List<Expense>> Sort(ExpensesSortRequest request )
        {
            switch (request.Field)
            {
                case "Id":
                    return await Sort(x => x.Id, request.IsAscendent);
                case "Amount":
                    return await Sort(x => x.Amount, request.IsAscendent);
                case "Description":
                    return await Sort(x => x.Description, request.IsAscendent);
                case "Date":
                    return await Sort(x => x.Date, request.IsAscendent);
                case "Type":
                    return await Sort(x => x.Type.Description, request.IsAscendent);
                default:
                    return await Sort(x => x.Id, request.IsAscendent);
            }
        }

        #endregion

        #region Own private helper methods

        private async Task<List<Expense>> Sort<T>(Expression<Func<Expense, T>> sortQuery, bool asc = true, int from = 0, int size = int.MaxValue)
        {
            return asc
                ? await _context.Expenses.Include(e => e.Type)
                    .OrderBy(sortQuery)
                    .Skip(from)
                    .Take(size)
                    .ToListAsync()
                : await _context.Expenses.Include(e => e.Type)
                    .OrderByDescending(sortQuery)
                    .Skip(from)
                    .Take(size)
                    .ToListAsync();
        }

        #endregion
    }
}
