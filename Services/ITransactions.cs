﻿using System.Collections.Generic;
using MoneyTrack.Models;

namespace MoneyTrack.Services
{
    public interface ITransactions
    {
        void Add(Transaction t);
        List<Transaction> All();
        void Update(Transaction t);
        Transaction Find(int id);
        bool Contains(Transaction t);
        void UpdateGroupIds(int oldId, int newId);
    }
}
