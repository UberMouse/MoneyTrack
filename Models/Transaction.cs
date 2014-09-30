using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoneyTrack.Importer.Models;

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
        public int GroupId { get; set; }
        [Required]
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
    }
}
