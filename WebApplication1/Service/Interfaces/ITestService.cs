using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Service.Interfaces
{
    /// <summary>
    /// Interface to encapsulate data layer integration services
    /// </summary>
    public interface ITestService<T> where T : TestBaseModel
    {
        Task<Expense> Save(Expense expense);

        Task<List<Expense>> GetAll();

        Task<Expense> Get(int? id);

        Task<Expense> Update(T expense);

        Task Remove(int id);

        bool Exists(int id);

    }

    
}
