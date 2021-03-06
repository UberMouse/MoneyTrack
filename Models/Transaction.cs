﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using MoneyTrack.BNZ.Models;

namespace MoneyTrack.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }

        [ForeignKey("Group")]
        [DefaultValue(1)]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public static Transaction From(BNZTransaction bnzTransaction)
        {
            return new Transaction
            {
                AccountId = bnzTransaction.accountId,
                Date = bnzTransaction.date,
                Description = bnzTransaction.description,
                Amount = bnzTransaction.formattedAmount,
                //First Group ('Untagged') should always have Id of 1
                GroupId = 1
            };
        }

        protected bool Equals(Transaction other)
        {
            return string.Equals(AccountId, other.AccountId) && string.Equals(Amount, other.Amount) && string.Equals(Description, other.Description) && Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (ObjectContext.GetObjectType(obj.GetType()) != GetType()) return false;
            
            return Equals((Transaction) obj);
        }

        public override string ToString()
        {
            return string.Format("AccountId: {0}, Date: {1}, Description: {2}, Amount: {3}, GroupId: {4}, Id: {5}", AccountId, Date, Description, Amount, GroupId, Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AccountId.GetHashCode();
                hashCode = (hashCode*397) ^ Amount.GetHashCode();
                hashCode = (hashCode*397) ^ Description.GetHashCode();
                return hashCode;
            }
        }
    }
}
