using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    /// <summary>
    /// Expense model
    /// </summary>
    public class Expense : TestBaseModel
    {

        public float Amount { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Navigation property foreign key (not necessary annotate cause convention )
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Navigation property ExpenseType
        /// </summary>
        public virtual ExpenseType Type { get; set; }

    }
}
