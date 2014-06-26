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
        public IEnumerable<TaskStep> Steps { get; set; }
        public TaskStep CurrStep { get; set; }

        public bool HasDatabaseCore { get; set; }
        public bool CanSubmit { get; set; }

        public TaskViewModel()
        {
            this.Steps = Repository.GetSteps();
            this.CurrStep = this.Steps.First();
        }
        public TaskViewModel(string currView) : this ()
        {
            this.CurrStep = this.Steps.Where(s => s.View == currView).Single();
        }
        public TaskViewModel(TaskViewModel that)
        {
            this.Steps = that.Steps != null ? that.Steps : Repository.GetSteps();

            CurrStep = that.CurrStep == null 
                ? this.Steps.First()
                : this.Steps.Where(s => s.View == that.CurrStep.View).Single();
        }
        public TaskStep GoToNextStep()
        {
            var currSeq = CurrStep.Sequence;
            var nextSeq = this.Steps.Where(s => !s.Hidden && s.Sequence > currSeq).OrderBy(s => s.Sequence).FirstOrDefault();

            if (nextSeq == null)
                return this.CurrStep = this.Steps.First();
            else
                return CurrStep = this.Steps.Where(s => s.Sequence == nextSeq.Sequence ).Single();
        }
    }
}
