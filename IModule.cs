using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace MoneyTrack
{
    interface IModule
    {
        void Load(Container c);
    }
}
