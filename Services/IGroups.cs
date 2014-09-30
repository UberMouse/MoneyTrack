using System.Collections.Generic;
using MoneyTrack.Models;

namespace MoneyTrack.Services
{
    public interface IGroups
    {
        Group Create(string name, string color);
        Group Find(int id);
        void Update(Group group);
        List<Group> All();
    }
}