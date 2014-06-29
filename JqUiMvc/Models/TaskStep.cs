using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    /// <summary>
    /// A task step is always in one of these states.
    /// Note that Errors and Modified don't tell whether the data
    /// is already in the database or not. I'm not sure it needs to, but...
    /// </summary>
    public enum StepState
    {
        /// <summary>
        /// No data exists.
        /// </summary>
        None,
        /// <summary>
        /// Data in the database - unmodified
        /// </summary>
        Complete,
        /// <summary>
        /// Data is invalid
        /// </summary>
        Errors,
        /// <summary>
        /// Data has been changed and is valid
        /// </summary>
        Modified
    }
    /// <summary>
    /// A single step in a task.
    /// TODO: 
    ///     a list of prerequisite steps
    ///     not sure if State should be here - probably NOT
    /// </summary>
    [DebuggerDisplay("{Sequence}.{View} {State}")]
    public class TaskStep
    {
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string View { get; set; }
        public string Description { get; set; }
        public StepState State { get; set; }
        public bool Hidden { get; set; }
    }
}
