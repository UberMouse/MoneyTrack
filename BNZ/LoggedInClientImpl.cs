using System.Collections.Generic;
using System.Net;
using MoneyTrack.BNZ.Models;
using MoneyTrack.Services;
using RestSharp;

namespace MoneyTrack.BNZ
{
    public class LoggedInClientImpl : ILoggedInClient
    {
        private readonly ITransactions _transactions;
        private readonly RestClient _client;

        public LoggedInClientImpl(ITransactions transactions, RestClient client)
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
    }
}
