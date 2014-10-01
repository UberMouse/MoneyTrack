using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
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
            _context.SaveChanges();
        }

        public Transaction Find(int id)
        {
           return  _context.Transactions.Find(id);
        }

        public bool Contains(Transaction t)
        {
            //Force everything into Local collection so object comparison can be performed
            _context.Transactions.Load();
            foreach (var ct in _context.Transactions.Local)
            {
                if (t.Equals(ct))
                    return true;
            }
            return false;
        }
    }
}
