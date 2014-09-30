using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyTrack.BNZ.Models;
using MoneyTrack.Models;

namespace MoneyTrack.Services
{
    public interface ITransactionsImporter
    {
        void Import(IEnumerable<BNZTransaction> transactions);
    }
}
