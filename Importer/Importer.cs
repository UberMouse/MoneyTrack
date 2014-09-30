using System.Linq;
using System.Net;
using MoneyTrack.Services;
using RestSharp;

namespace MoneyTrack.Importer
{
    public class Importer
    {
        private const string ROOT = "https://www.bnz.co.nz";
        private readonly ITransactions _transactions;

        public Importer(ITransactions transactions)
        {
            _transactions = transactions;
        }

        public LoggedInImporter Login(string accessid, string pw)
        {
            var client = new RestClient(ROOT)
            {
                CookieContainer = new CookieContainer(),           
            };

            // 1. Send login to get token and JSESSIONID
            var request = new RestRequest("ib/app/alogin/login", 
                                          Method.POST).AddParameter("accessid", accessid)
                                                      .AddParameter("password", pw);

            var response = client.Execute(request);

            //2. Send request to activate token
            var tokenRequest = new RestRequest(
                response.Headers.First(param => param.Name == "X-Location").Value.ToString()
            );
            client.Execute(tokenRequest);

            if (response.StatusCode != HttpStatusCode.OK) return null;
            else return new LoggedInImporter(_transactions, client);
        }
    }
}
