using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    public class GroupsController : AssertionHelper
    {
        private Container _container;
        private Controllers.Api.GroupsController _controller;

        public void SetUp()
        {
            _container = DependencyConfig.BuildContainer(c =>
            {
                c.Options.AllowOverridingRegistrations = true;

                c.Register<IGroups, TestGroups>(Lifestyle.Singleton);

                c.Options.AllowOverridingRegistrations = false;
            });

            

            _controller = _container.GetInstance<Controllers.Api.GroupsController>();
        }

        [Test]
        public void It_Works()
        {
            SetUp();
            var groupData = new Controllers.Api.GroupsController.GroupData
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
                new Controllers.Api.GroupsController.GroupData
                {
                    Name = "Exception Test - #",
                    Color = "#FFFFFF"
                },
                new Controllers.Api.GroupsController.GroupData
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
    }

    public class TestGroups : IGroups
    {
        private readonly List<Group> _groups = new List<Group>();

        public Group Create(string name, string color)
        {
            var group = new Group {Name = name, Color = color};
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
    }
}
