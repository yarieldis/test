using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.HttpRequests;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class ExpensesController : Controller
    {

        private readonly ExpenseService _service;

        public ExpensesController(ExpenseContext context)
        {
            _service = new ExpenseService(context);
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _service.Get(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_service.GetExpenseTypesDbSet(), "Id", "Description");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Description,Date,TypeId,Id")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _service.Save(expense);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_service.GetExpenseTypesDbSet(), "Id", "Description", expense.TypeId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _service.Get(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_service.GetExpenseTypesDbSet(), "Id", "Description", expense.TypeId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Amount,Description,Date,TypeId,Id")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_service.GetExpenseTypesDbSet(), "Id", "Description", expense.TypeId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _service.Get(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            await _service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _service.Exists(id);
        }


        // POST: Sort
        [HttpPost, ActionName("Sort")]
        [ValidateAntiForgeryToken]
       
        public async Task<List<Expense>> Sort([FromBody] ExpensesSortRequest request)
        {
           
                return await _service.Sort(request);
            

        }
    }
}
