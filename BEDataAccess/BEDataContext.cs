using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEDataAccess
{
    class BEDataContext : DbContext
    {
        public DbSet<SITES> Sites { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<ROOMS> Rooms { get; set; }
        public DbSet<SiteEvents> SiteEvents { get; set; }
    }
}
