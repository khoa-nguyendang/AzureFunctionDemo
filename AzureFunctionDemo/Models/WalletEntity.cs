using System.ComponentModel.DataAnnotations;

namespace AzureFunctionDemo.Models
{
    public class WalletEntity
    {
        [Key]
        public int Id { get; set; }
        public int Account { get; set; }
        public decimal Balance { get; set; }
        public double LastModified { get; set; }
    }
}
