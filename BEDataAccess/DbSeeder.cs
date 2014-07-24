using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEDataAccess
{
    class DbSeeder : DropCreateDatabaseAlways<BEDataContext>
    {
        protected override void Seed(BEDataContext context)
        {
            var sites = new Site[]{
                new Site{ID=8,Name="Site Eight"},
                new Site{ID=9,Name="Site Nine"},
            };

            foreach (var s in sites)
                context.Sites.Add(s);

            //All Sites will
            base.Seed(context);
        }
    }
}
