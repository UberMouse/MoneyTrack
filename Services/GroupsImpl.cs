using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
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

        public Group Add(Group group)
        {
            var newGroup = _context.Groups.Add(group);
            _context.SaveChanges();
            return newGroup;
        }

        public Group Find(int id)
        {
            return _context.Groups.Find(id);
        }

        public void Update(Group group)
        {
            _context.Groups.AddOrUpdate(group);
            _context.SaveChanges();
        }

        public List<Group> All()
        {
            return _context.Groups.ToList();
        }

        public void Delete(int id)
        {
            var group = Find(id);

            _context.Groups.Remove(group);

            _context.SaveChanges();
        }
    }
}