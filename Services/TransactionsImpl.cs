using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using MoneyTrack.Importer;
using MoneyTrack.Models;

namespace MoneyTrack.Services
{
    public class TransactionsImpl : ITransactions
    {
        private readonly DbContext _context;

        public TransactionsImpl(DbContext context)
        {
            _context = context;
        }

        public void Add(Transaction t)
        {
            _context.Transactions.Add(t);
            _context.SaveChanges();
        }

        public List<Transaction> All()
        {
           return  _context.Transactions.ToList();
        }

        public void Update(Transaction t)
        {
            _context.Transactions.AddOrUpdate(t);
        }

        public Transaction Find(int id)
        {
           return  _context.Transactions.Find(id);
        }
    }
}
