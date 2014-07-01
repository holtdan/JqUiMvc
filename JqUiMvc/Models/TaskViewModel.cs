using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace JqUiMvc.Models
{
    /// <summary>
    /// A task is a logical, higher level thing: i.e. Schedule a Visit
    /// It's made up of Steps. This class just deals with those basics.
    /// </summary>
    [DebuggerDisplay("{CurrStep.View} db={HasDatabaseCore} canSub={CanSubmit}")]
    public class TaskViewModel
    {
        /// <summary>
        /// ALL possible scheduling steps are here - some may be 'hidden'
        /// </summary>
        public IEnumerable<TaskStep> Steps { get; set; }
        /// <summary>
        /// The step on which the user is interacting.
        /// </summary>
        public TaskStep CurrStep { get; set; }

        public TaskViewModel()
        {
        }
        public TaskViewModel(string currView) //: this ()
        {
        }
        /// <summary>
        /// Returns the View that follows the given one. 
        /// Skips hidden and wraps to 1st step if at the last step.
        /// </summary>
        public string GetNextView(string afterThis1)
        {
            var currSeqNum = this.Steps.Where(s => s.View == afterThis1).Single().Sequence;
            var nextSeq = this.Steps.Where(s => !s.Hidden && s.Sequence > currSeqNum).OrderBy(s => s.Sequence).FirstOrDefault();

            return (nextSeq == null) ? this.Steps.First().View : nextSeq.View;
        }
    }
}
