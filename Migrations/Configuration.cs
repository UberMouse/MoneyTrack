using MoneyTrack.Models;

namespace MoneyTrack.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoneyTrack.DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MoneyTrack.DbContext context)
        {
            context.Groups.Add(new Group() { Id = 1, Color = "", Name = "Untagged" });
        }
    }
}
