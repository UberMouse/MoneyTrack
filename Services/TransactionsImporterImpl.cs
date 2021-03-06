﻿using System.Collections.Generic;
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
            var cTransactions = transactions.Select(Transaction.From).ToList();
            var toInsert = cTransactions.Where(t => !_transactions.Contains(t)).ToList();
            foreach(var t in toInsert)
                _transactions.Add(t);
        }
    }
}