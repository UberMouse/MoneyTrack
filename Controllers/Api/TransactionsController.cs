using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MoneyTrack.BNZ;
using MoneyTrack.Models;
using MoneyTrack.Services;

namespace MoneyTrack.Controllers.Api
{
    public class TransactionsController : ApiController
    {
        private readonly ITransactions _transactions;
        private readonly IClient _client;
        private readonly ITransactionsImporter _importer;


        public TransactionsController(ITransactions transactions, IClient client, ITransactionsImporter importer)
        {
            _transactions = transactions;
            _client = client;
            _importer = importer;
        }

        [HttpGet]
        public List<Transaction> Index()
        {
            return _transactions.All().OrderBy(t => t.Date).Reverse().ToList();
        }

        [HttpPost]
        public void Update(Transaction transaction)
        {
            _transactions.Update(transaction);
        }

        [HttpPost]
        public Transaction ChangeColor(ChangeColorData data)
        {
            var transaction = _transactions.Find(data.Id);

            transaction.GroupId = data.GroupId;
            _transactions.Update(transaction);

            return _transactions.Find(data.Id);
        }

        [HttpPost]
        public void Import(Credentials credentials)
        {
            var loggedInClient = _client.Login(credentials.AccessId, credentials.Password);
            _importer.Import(loggedInClient.Transactions());
        }

        public class Credentials
        {
            public string AccessId { get; set; }
            public string Password { get; set; }
        }

        public class ChangeColorData
        {
            public int Id { get; set; }
            public int GroupId { get; set; }
        }
    }
}
