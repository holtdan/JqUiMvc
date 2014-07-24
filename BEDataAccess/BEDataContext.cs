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
        public DbSet<Site> Sites { get; set; }
    }
}
