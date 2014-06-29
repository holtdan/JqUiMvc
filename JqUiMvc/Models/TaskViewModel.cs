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
            this.CurrStep = Repository.GetSteps().First();
        }
        public TaskViewModel(string currView) //: this ()
        {
            this.CurrStep = Repository.GetSteps().Where(s => s.View == currView).Single();
            this.CurrStep.State = Repository.Dbs[currView];
            this.HasDatabaseCore = Repository.Dbs["VisitInfo"] == StepState.Complete;
        }
    }
}
