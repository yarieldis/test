namespace WebApplication1.Models
{
    /// <summary>
    /// Base class for Entityies in the domain
    /// </summary>
    public abstract class TestBaseModel
    {
        /// <summary>
        /// Field Id using EntityframeworkCore naming convention for increments id
        /// </summary>
        public int Id { get; set; }

    }
}
