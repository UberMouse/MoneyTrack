using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web.Http;
using MoneyTrack.Services;
using Group = MoneyTrack.Models.Group;

namespace MoneyTrack.Controllers.Api
{
    public class GroupsController : ApiController
    {
        private readonly IGroups _groups;
        private readonly ITransactions _transactions;

        public GroupsController(IGroups groups, ITransactions transactions)
        {
            _groups = groups;
            _transactions = transactions;
        }

        [HttpPost]
        public Group Index(GroupData data)
        {
            if(!data.Valid()) throw new Exception("Color is invalid");
            var group = new Group
            {
                Color = data.Color,
                Name = data.Name
            };
            return _groups.Add(group);
        }

        [HttpGet]
        public Group Index(int id)
        {
            return _groups.Find(id);
        }

        [HttpPost]
        public void Delete(DeleteData data)
        {
            _transactions.UpdateGroupIds(data.Id, 1);
            _groups.Delete(data.Id);
        }

        [HttpGet]
        public List<Group> Index()
        {
            return _groups.All();
        }

        public class GroupData
        {
            private static readonly Regex ColorValidator = new Regex(@"[a-zA-Z0-9]");
            public string Name { get; set; }
            public string Color { get; set; }

            public bool Valid()
            {
                if (!ColorValidator.IsMatch(Color)) return false;
                return Color.Length == 3 || Color.Length == 6;
            }
        }

        public class DeleteData
        {
            public int Id { get; set; }
        }
    }
}
