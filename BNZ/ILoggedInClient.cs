using System.Collections.Generic;
using MoneyTrack.BNZ.Models;

namespace MoneyTrack.BNZ
{
    public interface ILoggedInClient
    {
        IEnumerable<BNZTransaction> Transactions();
    }
}