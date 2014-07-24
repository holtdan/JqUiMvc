using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEDataAccess
{
    public class Engine
    {
        public void Go()
        {
            using (var dc = new BEDataContext())
            {
                //var sites = dc.Sites;
                dc.Sites.Add(new Site { ID = 8, Name = "Site Eight" });
                dc.Sites.Add(new Site { ID = 9, Name = "Site Nine" });
                dc.SaveChanges();
            }
        }
    }
}
