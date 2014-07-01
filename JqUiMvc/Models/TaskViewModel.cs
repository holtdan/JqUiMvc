using JqUiMvc.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    /// <summary>
    /// A task is a logical, higher level thing: i.e. Schedule a Visit
    /// It's made up of Steps. 
    /// </summary>
    [DebuggerDisplay("{CurrStep.View} db={HasDatabaseCore} canSub={CanSubmit}")]
    public class TaskViewModel
    {
        public VisitTypeScheduleConfig VisitTypeConfig { get; set; }
        public IEnumerable<TaskStep> Steps { get; set; }
        public TaskStep CurrStep { get; set; }

        /// <summary>
        /// Data for this task is already in the database - i.e. we're updating
        /// </summary>
        public bool HasDatabaseCore { get; set; }
        /// <summary>
        /// Very Schedule-Visit specific ... it's a start
        /// </summary>
        public bool CanSubmit { get; set; }

        public TaskViewModel()
        {
            this.SetSchedConfig();
            this.CurrStep = this.Steps.First();
        }
        public TaskViewModel(string currView) //: this ()
        {
            this.SetSchedConfig();
            this.CurrStep = this.Steps.Where(s => s.View == currView).Single();
            this.CurrStep.State = Repository.Dbs[currView];
            this.HasDatabaseCore = Repository.Dbs["VisitInfo"] == StepState.Complete;
        }
        /// <summary>
        /// Returns the View that follows the given one. 
        /// Skips hidden and wraps to 1st step if at the last step.
        /// </summary>
        /// <param name="afterThis1"></param>
        /// <returns></returns>
        public string GetNextView(string afterThis1)
        {
            var currSeqNum = this.Steps.Where(s => s.View == afterThis1).Single().Sequence;
            var nextSeq = this.Steps.Where(s => !s.Hidden && s.Sequence > currSeqNum).OrderBy(s => s.Sequence).FirstOrDefault();

            return (nextSeq == null) ? this.Steps.First().View : nextSeq.View;
        }
        /// <summary>
        /// Gets master Visit-Scheduling steps list and applies current Visit-Type config
        /// </summary>
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
