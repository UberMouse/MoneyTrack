using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleInjector;

namespace MoneyTrack.BNZ
{
    public class BNZModule : IModule
    {
        public void Load(Container c)
        {
            c.Register<IClient, ClientImpl>();
        }
    }
}