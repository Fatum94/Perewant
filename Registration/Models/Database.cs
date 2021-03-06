using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Registration.Models
{

    public class Database : DbContext
    {
        public Database() : base("Database") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Kompressor> Compressor { get; set; }
        public DbSet<Sverdlovuna> Sverdlovunas { get; set; }
        public DbSet<History> Histories { get; set; }
    }

    public class DbInit : DropCreateDatabaseIfModelChanges<Database>
    {
        protected override void Seed(Database context)
        {
            base.Seed(context);
        }
    }
}
