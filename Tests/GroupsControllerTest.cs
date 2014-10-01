using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FluentAssertions;
using MoneyTrack.Controllers.Api;
using MoneyTrack.Services;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SimpleInjector;
using Container = SimpleInjector.Container;
using MoneyTrack.Models;

namespace MoneyTrack.tests
{
    [TestFixture]
    public class GroupsControllerTest : AssertionHelper
    {
        private Container _container;
        private GroupsController _controller;

        public void SetUp()
        {
            _container = DependencyConfig.BuildContainer(c =>
            {
                c.Options.AllowOverridingRegistrations = true;

                c.Register<IGroups, TestGroups>(Lifestyle.Singleton);
                c.Register<ITransactions, TestTransactions>(Lifestyle.Singleton);

                c.Options.AllowOverridingRegistrations = false;
            });

            

            _controller = _container.GetInstance<GroupsController>();
        }

        [Test]
        public void It_Works()
        {
            SetUp();
            var groupData = new GroupsController.GroupData
            {
                Name = "Test",
                Color = "3C763D"
            };

            var group = _controller.Index(groupData);

            Assert.AreEqual(groupData.Name, group.Name);
            Assert.AreEqual(groupData.Color, group.Color);
            Assert.AreEqual(_controller.Index(group.Id), group);
        }

        [Test]
        public void Throw_Exception_If_Color_Is_Not_Alpha_Numeric()
        {
            SetUp();
            var groupData = new[]
            {
                new GroupsController.GroupData
                {
                    Name = "Exception Test - #",
                    Color = "#FFFFFF"
                },
                new GroupsController.GroupData
                {
                    Name = "Exception Test -  ",
                    Color = "FFF FFF"
                }
            };

            foreach (var d in groupData)
            {
                Assert.Throws<Exception>(() => _controller.Index(d));
            }
        }

        [Test]
        public void When_A_Group_Is_Deleted_Transactions_In_It_Get_Placed_Back_In_Untagged()
        {
            SetUp();
            
            var transactionService = _container.GetInstance<ITransactions>();
            var groupService = _container.GetInstance<IGroups>();

            var transactions = new[]
            {
                new Transaction
                {
                    Description = "Group 2",
                    GroupId = 2
                },
                new Transaction
                {
                    Description = "Group 2",
                    GroupId = 2
                },
                new Transaction
                {
                    Description = "Group 1",
                    GroupId = 1
                },
                new Transaction
                {
                    Description = "Group 3",
                    GroupId = 3
                }
            };
            var groups = new[]
            {
                new Group
                {
                    Name = "Group 1",
                    Id = 1
                },
                new Group
                {
                    Name = "Group 2",
                    Id = 2
                },
                new Group
                {
                    Name = "Group 3",
                    Id = 3
                },
            };
            foreach(var t in transactions)
                transactionService.Add(t);
            foreach (var g in groups)
                groupService.Add(g);

            _controller.Delete(new GroupsController.DeleteData{Id = 2});

            transactionService.All().Count(t => t.GroupId == 1).Should().Be(3);
        }
    }

    public class TestGroups : IGroups
    {
        private readonly List<Group> _groups = new List<Group>();

        public Group Add(Group group)
        {
            _groups.Add(group);
            return group;
        }

        public Group Find(int id)
        {
            return _groups.First(g => g.Id == id);
        }

        public void Update(Group group)
        {
            _groups.RemoveAll(g => g.Id == group.Id);
            _groups.Add(group);
        }

        public List<Group> All()
        {
            return _groups;
        }

        public void Delete(int id)
        {
            _groups.Remove(_groups.Find(g => g.Id == id));
        }
    }
}
