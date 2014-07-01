using JqUiMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    public class ScheduleVisitTaskViewModel : TaskViewModel
    {
        /// <summary>
        /// This is the configuration info for the Visit Type being scheduled.
        /// </summary>
        public VisitTypeScheduleConfig VisitTypeConfig { get; set; }

        public int? VisitID { get; set; }
        public string VisitName { get; set; }
        public string Location { get; set; }
        public string VisitType { get; set; }
        public string MainRoom { get; set; }
        public int? NumAttendees { get; set; }

        /// <summary>
        /// Data for this task is already in the database - i.e. we're updating
        /// </summary>
        public bool HasDatabaseCore { get; set; }
        /// <summary>
        /// Very Schedule-Visit specific ... it's a start
        /// </summary>
        public bool CanSubmit { get; set; }

        public ScheduleVisitTaskViewModel() 
            : base() 
        {
            SetSchedConfig();
            this.CurrStep = this.Steps.First();
        }
        public ScheduleVisitTaskViewModel(string currView) 
            : base(currView)
        {
            SetSchedConfig();
            this.CurrStep = this.Steps.Where(s => s.View == currView).Single();

            this.HasDatabaseCore = Repository.Dbs["VisitInfo"] == StepState.Complete;
        }

        void SetSchedConfig()
        {
            this.Steps = Repository.GetSteps();

            this.VisitTypeConfig = Repository.GetVisitTypeScheduleConfig();

            MaybeHide("Customers", this.VisitTypeConfig.HideCustomers);
            MaybeHide("Opportunities", this.VisitTypeConfig.HideOpportunities);
            MaybeHide("Reason", this.VisitTypeConfig.HideReason);
            MaybeHide("Logistics", this.VisitTypeConfig.HideLogistics);
        }
        /// <summary>
        /// ...just to avoid repeating this line 3 times.
        /// </summary>
        void MaybeHide(string view, bool hide)
        {
            this.Steps.Where(s => s.View == view).Single().Hidden = hide;
        }
    }
}
