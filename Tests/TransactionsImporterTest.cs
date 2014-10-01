using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyTrack.BNZ.Models;
using MoneyTrack.Models;
using MoneyTrack.Services;
using NUnit.Framework;
using SimpleInjector;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MoneyTrack.tests
{
    [TestFixture]
    public class TransactionsImporterTest
    {
        private Container _container;
        private ITransactionsImporter _importer;

        public void SetUp()
        {
            _container = DependencyConfig.BuildContainer(c =>
            {
                c.Options.AllowOverridingRegistrations = true;

                c.Register<ITransactions, TestTransactions>(Lifestyle.Singleton);

                c.Options.AllowOverridingRegistrations = false;
            });

            _importer = _container.GetInstance<ITransactionsImporter>();
        }

        [Test]
        public void Only_Imports_New_Transactions()
        {
            SetUp();

            var duplicateTransaction = new BNZTransaction
            {
                accountId = "1",
                amount = 50.00f,
                date = DateTime.Now,
                description = "Test2"
            };
            var service = _container.GetInstance<ITransactions>();
            service.Add(Transaction.From(duplicateTransaction));

            var transactions = new[]
            {
                duplicateTransaction,
                new BNZTransaction
                {
                    accountId = "1",
                    amount = 5.00f,
                    date = DateTime.Now,
                    description = "Test3"
                },
                duplicateTransaction
            };

            _importer.Import(transactions);

            Assert.AreEqual(service.All().Count, 2);
        }
    }
}
