using System.Collections.Generic;
using System.Linq;
using System.Net;
using MoneyTrack.Importer.Models;
using MoneyTrack.Models;
using MoneyTrack.Services;
using RestSharp;

namespace MoneyTrack.Importer
{
    public class LoggedInImporter
    {
        private readonly ITransactions _transactions;
        private readonly RestClient _client;

        internal LoggedInImporter(ITransactions transactions, RestClient client)
        {
            _transactions = transactions;
            _client = client;
        }

        public IEnumerable<BNZTransaction> Transactions()
        {
            var request = new RestRequest("ib/api/transactions", Method.GET);

            var response = _client.Execute<List<BNZTransaction>>(request);

            if (response.StatusCode != HttpStatusCode.OK) return null;
            return response.Data;
        }

        public void Import()
        {
            foreach(var t in Transactions().Select(Transaction.From))
                _transactions.Add(t);
        }
    }
}
