using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEDataAccess
{
    public class Engine
    {
        public Engine()
        {
            Database.SetInitializer(new DbSeeder());
        }
        public void Go()
        {
            using (var dc = new BEDataContext())
            {
                var sites = dc.Sites;
                //dc.Sites.Add(new Site { ID = 81, Name = "Site Eight1" });
                //dc.Sites.Add(new Site { ID = 91, Name = "Site Nine1" });
                //dc.SaveChanges();
            }
        }
    }
}
