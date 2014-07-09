using JqUiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Data
{
    public class Repository : IDisposable
    {
        public static void Reset()
        {
            Repository.Dbs.Clear();
            Repository.VisitModel = null;
        }
        public const int DefaultSiteID = 9;

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
    }
}
