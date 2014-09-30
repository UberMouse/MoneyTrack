using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using SimpleInjector;

namespace MoneyTrack.Services
{
    public class ServicesModule : IModule
    {
        public void Load(Container c)
        {
            c.Register<IGroups, GroupsImpl>();
            c.Register<ITransactions, TransactionsImpl>();
        }
    }
}