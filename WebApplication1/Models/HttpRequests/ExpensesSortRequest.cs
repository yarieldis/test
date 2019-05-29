namespace WebApplication1.Models.HttpRequests
{
    /// <summary>
    /// Model request for sorting expenses list 
    /// </summary>
    public class ExpensesSortRequest
    {
        public string Field { get; set; }

        public bool IsAscendent { get; set; }

        public int From { get; set; } = 0;

        public int Size { get; set; } = int.MaxValue;
    }
}
