using System;

namespace MoneyTrack.BNZ.Models
{
    public class BNZTransaction
    {
        public string accountId { get; set; }
        public float amount { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public string formattedAmount { get; set; }
        // No need to deserialize this currently, here for completeness
        //public FromStatementDetails fromStatementDetails { get; set; }
        public string matchingTransactionAccountId { get; set; }
        public string otherPartyName { get; set; }
        public bool realtime { get; set; }
        public string transactionTypeCode { get; set; }
        public string transactionTypeDescription { get; set; }
    }
}