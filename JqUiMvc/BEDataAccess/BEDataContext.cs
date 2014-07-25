using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEDataAccess
{
    public class BEDataContext : DbContext
    {
        public BEDataContext() : base("BEDataContext") { }

        public DbSet<SITES> Sites { get; set; }
        public DbSet<EventType> VisitTypes { get; set; }
        public DbSet<ROOMS> Rooms { get; set; }
        public DbSet<SiteEvents> SiteEvents { get; set; }
        public DbSet<USERS> Users { get; set; }
        public DbSet<VISITS> Visits { get; set; }
    }
}
