using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.HttpRequests;
using WebApplication1.Service;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ExpensesApiController : ControllerBase
    {
        private readonly ExpenseService _service;

        public ExpensesApiController(ExpenseContext context)
        {
            _service = new ExpenseService(context);
        }

        [HttpGet]
        public List<Expense> Get()
        {
            return _service.GetAll().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        // POST: api/ExpensesApi/sort
        [HttpPost ("sort")]
        public List<Expense> SortExpenses([FromBody] ExpensesSortRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Bad request");
            }

            return _service.Sort(request).ConfigureAwait(false).GetAwaiter().GetResult();
        }

    }
}