using System.Collections.Generic;
using System.Linq;
using MoneyTrack.BNZ.Models;
using MoneyTrack.Models;

namespace MoneyTrack.Services
{
    class TransactionsImporterImpl : ITransactionsImporter
    {
        private readonly ITransactions _transactions;

        public TransactionsImporterImpl(ITransactions transactions)
        {
            _transactions = transactions;
        }

        public void Import(IEnumerable<BNZTransaction> transactions)
        {
            foreach(var t in transactions.Select(Transaction.From).Where(t => !_transactions.Contains(t)))
                _transactions.Add(t);
        }
    }
}