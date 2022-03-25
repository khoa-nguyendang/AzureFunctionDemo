using AzureFunctionDemo.Efcore;
using AzureFunctionDemo.Models;
using AzureFunctionDemo.Usecases.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureFunctionDemo.Usecases
{
    public class TransactionVerifier : ITransactionVerifier
    {
        private readonly TransactionDbContext _context;
        private readonly ILogger _logger;

        public TransactionVerifier(ILoggerFactory loggerFactory, TransactionDbContext context)
        {
            _logger = loggerFactory.CreateLogger<FunctionController>();
            this._context = context;
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Return remaining balace of current account after record latest transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<decimal> RecordTransaction(TransactionInputDTO transactionInput)
        {
            decimal remainingBalance = 0;
            using(var dbt = _context.Database.BeginTransaction())
            {

                var wallet = _context.Wallets.FirstOrDefault(w => w.Account == transactionInput.Account);
                if (wallet == null)
                {
                    throw new Exception("Wallet not found anymore.");
                }

                remainingBalance = wallet.Balance = wallet.Balance - transactionInput.Amount;
                _ = await _context.Transactions.AddAsync(new TransactionEntity {
                    Amount = transactionInput.Amount,
                    Account = transactionInput.Account,
                    Direction = transactionInput.Direction,
                });
                _context.Wallets.Update(wallet);
                _context.SaveChanges();
                dbt.Commit();
            }
            return remainingBalance;
        }

        /// <summary>
        /// Verify Account is exists or not
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> VerifyAccountAsync(TransactionInputDTO transactionInput)
        {
            return _context.Wallets.Any(w => w.Account == transactionInput.Account);
        }

        /// <summary>
        /// Verify balance of existing account
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> VerifyAccountBalanceAsync(TransactionInputDTO transactionInput)
        {
            if (transactionInput == null)
            {
                throw new Exception("Invalid Model");
            }

            if (transactionInput.Amount <= 0)
            {
                throw new Exception("Invalid spending");
            }

            return _context.Wallets.Any(w => w.Account == transactionInput.Account && w.Balance > transactionInput.Amount);
        }
    }
}
