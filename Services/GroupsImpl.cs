using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MoneyTrack.Models;

namespace MoneyTrack.Services
{
    public class GroupsImpl : IGroups
    {
        private readonly DbContext _context;

        public GroupsImpl(DbContext context)
        {
            _context = context;
        }

        public Group Create(string name, string color)
        {
            var group = new Group() {Name = name, Color = color};

            _context.Groups.Add(group);
            _context.SaveChangesAsync();

            return group;
        }

        public Group Find(int id)
        {
            return _context.Groups.Find(id);
        }

        public void Update(Group group)
        {
            _context.Groups.AddOrUpdate(group);
        }

        public List<Group> All()
        {
            return _context.Groups.ToList();
        }
    }
}