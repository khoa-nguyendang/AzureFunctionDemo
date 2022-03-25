using AzureFunctionDemo.Models;

namespace AzureFunctionDemo.Usecases.Interfaces
{
    public interface ITransactionVerifier
    {
        Task<bool> VerifyAccountAsync(TransactionInputDTO transactionInput);
        Task<bool> VerifyAccountBalanceAsync(TransactionInputDTO transactionInput);
        Task<decimal> RecordTransaction(TransactionInputDTO transactionInput);
    }
}
