using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyTrack.Models;
using MoneyTrack.Services;
using SimpleInjector;

namespace MoneyTrack.tests
{
    [TestClass]
    public class TransactionsControllerTests
    {
        private Container _container;
        private Controllers.Api.TransactionsController _controller;

        public void SetUp()
        {
            _container = DependencyConfig.BuildContainer(c =>
            {
                c.Options.AllowOverridingRegistrations = true;

                c.Register<ITransactions, TestTransactions>(Lifestyle.Singleton);

                c.Options.AllowOverridingRegistrations = false;
            });



            _controller = _container.GetInstance<Controllers.Api.TransactionsController>();
        }

        [TestMethod]
        public void Import_Adds_Retrieved_BNZ_Transactions()
        {
            SetUp();

            var credentials = new Controllers.Api.TransactionsController.Credentials
            {
                AccessId = Environment.GetEnvironmentVariable("BNZ_AID"),
                Password = Environment.GetEnvironmentVariable("BNZ_PW")
            };

            _controller.Import(credentials);

            Assert.IsTrue(_container.GetInstance<ITransactions>().All().Count != 0);
        }
    }

    public class TestTransactions : ITransactions
    {
        private readonly List<Transaction> _transactions = new List<Transaction>(); 

        public void Add(Transaction t)
        {
            _transactions.Add(t);
        }

        public List<Transaction> All()
        {
            return _transactions;
        }

        public void Update(Transaction t)
        {
            _transactions.Remove(t);
            _transactions.Add(t);
        }

        public Transaction Find(int id)
        {
            return _transactions.First(t => t.Id == id);
        }

        public bool Contains(Transaction t)
        {
            return _transactions.Contains(t);
        }
    }
}
