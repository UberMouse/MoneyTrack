using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoneyTrack.Controllers.Api;
using SimpleInjector;

namespace MoneyTrack.Controllers
{
    public class Controllers : IModule
    {
        public void Load(Container c)
        {
            c.Register<HomeController>();
            c.Register<GroupsController>();
            c.Register<TransactionsController>();
        }
    }
}