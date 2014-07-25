using JqUiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEDataAccess.Models;

namespace JqUiMvc.Data
{
    public class Repository : IDisposable
    {
        public static void Reset()
        {
            Repository.Dbs.Clear();
            Repository.VisitModel = null;
        }
        public const int DefaultSiteID = 1;

        public static VisitModel VisitModel;

        public static IEnumerable<VisitType> GetVisitTypes(int siteID)
        {
            if (siteID == DefaultSiteID)
                return new VisitType[]{
                    new VisitType{ID=1,Name="VisType One"},
                    new VisitType{ID=2,Name="VisType Two"},
                    new VisitType{ID=3,Name="VisType Three"},
            };
            else
                return new VisitType[]{
                new VisitType{ID=3,Name="VisType Three"},
                new VisitType{ID=4,Name="VisType Four"},
            };
        }
        public static IEnumerable<Site> GetSites()
        {
            return new Site[]{
                new Site{ID=8,Name="Site Eight"},
                new Site{ID=9,Name="Site Nine"},
            };
        }
        public static IEnumerable<Room> GetRooms()
        {
            return new Room[]{
                new Room{ID=51,SiteID=9,Name="Room AAA"},
                new Room{ID=52,SiteID=9,Name="Room BBB"},
                new Room{ID=53,SiteID=9,Name="Room CCC"},
                new Room{ID=63,SiteID=8,Name="Room OOO"},
                new Room{ID=64,SiteID=8,Name="Room PPP"},
            };
        }
        public static int? ID;
        public static Dictionary<string, StepState> Dbs = new Dictionary<string, StepState>();
        //public static bool HasDatabaseCore { get { return ID != null; } }
        public static VisitTypeScheduleConfig GetVisitTypeScheduleConfig()//int id
        {
            return new VisitTypeScheduleConfig
            {
                VisitTypeID = 22,
                Name = "Briefing",
                HideReason = true
            };
        }
        public static IEnumerable<TaskStep> GetSteps()
        {
            if (Dbs.Count() == 0)
                foreach (var s in new string[] { "VisitInfo", "Owner", "Customers", "Opportunities", "Attendees", "Reason", "Logistics", "Documents" })
                    Dbs[s] = StepState.None;

            var steps = new TaskStep[]{
                new TaskStep{Sequence=0,Name="Info",View="VisitInfo"},
                new TaskStep{Sequence=1,Name="Owner",View="Owner"},
                new TaskStep{Sequence=2,Name="Customers",View="Customers"},
                new TaskStep{Sequence=3,Name="Opportunities",View="Opportunities"},
                new TaskStep{Sequence=4,Name="Attendees",View="Attendees"},
                new TaskStep{Sequence=5,Name="Reason",View="Reason"},
                new TaskStep{Sequence=6,Name="Logistics",View="Logistics"},
                new TaskStep{Sequence=7,Name="Documents",View="Documents"},
            };

            foreach (var s in steps) s.State = Dbs[s.View];

            return steps;
        }
        public void Dispose()
        {
        }

        public static IEnumerable<AgendaCategory> GetAgendaTopicCategories(bool forAM = false, bool active = true)
        {
            var all = new AgendaCategory[]{
                new AgendaCategory{ID=1,Name="Category Sample X", IsActive=false,
                Items = new AgendaTopic[]{}},
                new AgendaCategory{ID=2,Name="Category Meal 1", IsActive=true, DisplayOrder=9, AllowAM=true, IsMeal=true,
                Items = new AgendaTopic[]{
                    new AgendaTopic{ID=21,Name="Dinner", DisplayOrder=3, AllowAM=true, IsMeal=true},
                    new AgendaTopic{ID=22,Name="Lunch", DisplayOrder=2, AllowAM=false, IsMeal=true},
                    new AgendaTopic{ID=23,Name="Breakfast", DisplayOrder=1, AllowAM=true, IsMeal=true},
                }},
                new AgendaCategory{ID=3,Name="Category Sample 2", IsActive=true, DisplayOrder=3, AllowAM=false, IsMeal=false,
                Items = new AgendaTopic[]{
                    new AgendaTopic{ID=31,Name="Topic N", DisplayOrder=3, AllowAM=true},
                    new AgendaTopic{ID=32,Name="Topic O", DisplayOrder=2, AllowAM=false},
                    new AgendaTopic{ID=33,Name="Topic P", DisplayOrder=1, AllowAM=true},
                }},
                new AgendaCategory{ID=4,Name="Category Test 1", IsActive=true, DisplayOrder=1, AllowAM=true, IsMeal=false,
                Items = new AgendaTopic[]{
                    new AgendaTopic{ID=41,Name="Topic X", DisplayOrder=3, AllowAM=true},
                    new AgendaTopic{ID=42,Name="Topic Y", DisplayOrder=2, AllowAM=false},
                    new AgendaTopic{ID=43,Name="Topic Z", DisplayOrder=1, AllowAM=true},
                }},
            };

            var cats = from c in all
                       where (!active || active && c.IsActive) && (!forAM || c.AllowAM)
                       orderby c.DisplayOrder
                       select c;
            foreach ( var c in cats )
                c.Items = c.Items.Where(r => !forAM || r.AllowAM).OrderBy(r => r.DisplayOrder).ToList();

            return cats;
        }
    }
}
