using System.Collections.Generic;
using System.Web.Http;
using MoneyTrack.Models;
using MoneyTrack.Services;

namespace MoneyTrack.Controllers.Api
{
    public class TransactionsController : ApiController
    {
        private readonly ITransactions _transactions;
        private readonly IGroups _groups;
        private readonly Importer.Importer _importer;


        public TransactionsController(ITransactions transactions, IGroups groups, Importer.Importer importer)
        {
            _transactions = transactions;
            _groups = groups;
            _importer = importer;
        }

        [HttpGet]
        public List<Transaction> Index()
        {
            return _transactions.All();
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
            var importer = _importer.Login(credentials.AccessId, credentials.Password);
            importer.Import();
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
