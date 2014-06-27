using JqUiMvc.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    [DebuggerDisplay("{CurrStep.View} db={HasDatabaseCore} canSub={CanSubmit}")]
    public class TaskViewModel
    {
        public TaskStep CurrStep { get; set; }

        public bool HasDatabaseCore { get; set; }
        public bool CanSubmit { get; set; }

        public TaskViewModel()
        {
            this.CurrStep = Repository.GetSteps().First();
        }
        public TaskViewModel(string currView) : this ()
        {
            this.CurrStep = Repository.GetSteps().Where(s => s.View == currView).Single();
            this.CurrStep.State = Repository.Dbs[currView];
            this.HasDatabaseCore = Repository.Dbs["VisitInfo"] == StepState.Complete;
        }
    }
}
