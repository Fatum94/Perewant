using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Registration.Models
{

    public class Entities : DbContext
    {
        public Entities() : base("Entities") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Kompressor> Compressor { get; set; }
    }

    public class DbInit : DropCreateDatabaseIfModelChanges<Entities>
    {
        protected override void Seed(Entities context)
        {
            base.Seed(context);
        }
    }
}
