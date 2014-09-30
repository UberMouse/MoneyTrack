using System.Collections.Generic;
using System.Web.Http;
using MoneyTrack.Models;
using MoneyTrack.Services;

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
            public string Name { get; set; }
            public string Color { get; set; }
        }
    }
}
