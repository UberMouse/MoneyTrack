using System.Collections.Generic;
using MoneyTrack.Models;

namespace MoneyTrack.Services
{
    public interface IGroups
    {
        Group Find(int id);
        void Update(Group group);
        List<Group> All();
        void Delete(int id);
        Group Add(Group group);
    }
}