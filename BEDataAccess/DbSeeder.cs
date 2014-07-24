using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEDataAccess
{
    class DbSeeder : DropCreateDatabaseIfModelChanges<BEDataContext> //DropCreateDatabaseAlways<BEDataContext>
    {
        protected override void Seed(BEDataContext dc)
        {
            LoadSites(dc);

            base.Seed(dc);
        }
        BEDataContext LoadSites ( BEDataContext dc )
        {
            var sites = new SITES[]{
                new SITES{SiteName="Site A",DateFormat="mm/dd/yyyy", SiteType="E", DefaultStartTime_FullDay="9:00", DefaultEndTime_FullDay="17:00", DefaultStartTime_AM="8:30", DefaultEndTime_AM="12:00", DefaultStartTime_PM="12:00",  DefaultEndTime_PM="16:30", MinimumVisitLength=0.5F, MaximumVisitLength=22, MaxVisitsPerDay=2},
                new SITES{SiteName="Site B",DateFormat="dd/mm/yyyy", SiteType="E", DefaultStartTime_FullDay="8:00", DefaultEndTime_FullDay="16:00", DefaultStartTime_AM="9:30", DefaultEndTime_AM="13:00", DefaultStartTime_PM="13:00",  DefaultEndTime_PM="17:00", MinimumVisitLength=1, MaximumVisitLength=3, MaxVisitsPerDay=1},
            };

            sites[0].Rooms = new ROOMS[] { 
                new ROOMS{ROOMNAME="Room A-1",OCCUPANCY=5,ACTIVEFLAG=-1},
                new ROOMS{ROOMNAME="Room A-2",OCCUPANCY=15,ACTIVEFLAG=-1},
                new ROOMS{ROOMNAME="Room A-3",OCCUPANCY=25,ACTIVEFLAG=-1},
            };
            sites[1].Rooms = new ROOMS[] { 
                new ROOMS{ROOMNAME="Room B-1",OCCUPANCY=5,ACTIVEFLAG=-1},
                new ROOMS{ROOMNAME="Room B-2",OCCUPANCY=15,ACTIVEFLAG=-1},
                new ROOMS{ROOMNAME="Room B-3",OCCUPANCY=25,ACTIVEFLAG=-1},
            };

            foreach (var s in sites)
                dc.Sites.Add(s);

            return dc;
        }
    }
}
