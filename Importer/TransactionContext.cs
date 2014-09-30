using System.Data.Entity;
using MoneyTrack.Importer.Models;
using MoneyTrack.Models;

namespace MoneyTrack.Importer
{
    public class TransactionContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
    }
}
