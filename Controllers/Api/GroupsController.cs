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

        public GroupsController(IGroups groups)
        {
            _groups = groups;
        }

        [HttpPost]
        public Group Index(GroupData data)
        {
            if(!data.Valid()) throw new Exception();
           return _groups.Create(data.Name, data.Color);
        }

        [HttpGet]
        public Group Index(int id)
        {
            return _groups.Find(id);
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
    }
}
