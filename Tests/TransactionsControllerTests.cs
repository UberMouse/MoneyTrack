using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyTrack.Controllers.Api;
using MoneyTrack.Models;
using MoneyTrack.Services;
using NUnit.Framework;
using SimpleInjector;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace MoneyTrack.tests
{
    [TestFixture]
    public class TransactionsControllerTests
    {
        private Container _container;
        private TransactionsController _controller;

        public void SetUp()
        {
            _container = DependencyConfig.BuildContainer(c =>
            {
                c.Options.AllowOverridingRegistrations = true;

                c.Register<ITransactions, TestTransactions>(Lifestyle.Singleton);

                c.Options.AllowOverridingRegistrations = false;
            });

            _controller = _container.GetInstance<TransactionsController>();
        }

        [Test]
        public void Import_Adds_Retrieved_BNZ_Transactions()
        {
            SetUp();

            var credentials = new TransactionsController.Credentials
            {
                AccessId = Environment.GetEnvironmentVariable("BNZ_AID"),
                Password = Environment.GetEnvironmentVariable("BNZ_PW")
            };

            _controller.Import(credentials);

            Assert.IsTrue(_container.GetInstance<ITransactions>().All().Count != 0);
        }

        [Test]
        public void Returns_List_Of_Transactions_Ordered_By_Time()
        {
            SetUp();

            var transactions = new[]
            {
                new Transaction
                {
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(2))
                },
                new Transaction
                {
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(3))
                },
                new Transaction
                {
                    Date = DateTime.Now
                },
                new Transaction
                {
                    Date = DateTime.Now.AddDays(1)
                },
                new Transaction
                {
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(6))
                }
            };

            var service = _container.GetInstance<ITransactions>();

            foreach (var t in transactions)
                service.Add(t);

            var recievedTransactions = _controller.Index();
            var expectedTransactions = recievedTransactions.OrderBy(t => t.Date).Reverse().ToList();

            foreach (var pair in recievedTransactions.Zip(expectedTransactions, Tuple.Create))
                Assert.AreEqual(pair.Item1, pair.Item2);
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

        public void UpdateGroupIds(int oldId, int newId)
        {
            foreach (var t in _transactions.Where(t => t.GroupId == oldId))
                t.GroupId = newId;
        }
    }
}
