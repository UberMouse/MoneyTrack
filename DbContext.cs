using System.Data.Entity;
using MoneyTrack.Models;

namespace MoneyTrack
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
