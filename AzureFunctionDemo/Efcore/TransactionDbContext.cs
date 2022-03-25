using AzureFunctionDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AzureFunctionDemo.Efcore
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WalletEntity>()
                .HasData(new WalletEntity()
                {
                    Account = 1001,
                    Balance = 99999999999,
                    Id = 1,
                    LastModified = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                }) ;
            
        }
    }
}
