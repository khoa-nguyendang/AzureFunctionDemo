using System.ComponentModel.DataAnnotations;

namespace AzureFunctionDemo.Models
{
    public class TransactionEntity
    {
        [Key]
        public int Id { get; set; }
        public int Account { get; set; }
        public string Direction { get; set; }
        public decimal Amount { get; set; }
    }
}
