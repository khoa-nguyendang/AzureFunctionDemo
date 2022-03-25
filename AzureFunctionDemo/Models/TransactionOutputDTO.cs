using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionDemo.Models
{
    public class TransactionOutputDTO
    {
	    public decimal BalanceAmount { get; set; }
        public string Direction { get; set; }
        public int Account { get; set; }
    }
}
