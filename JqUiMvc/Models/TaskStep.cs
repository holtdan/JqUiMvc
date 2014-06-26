using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    public enum StepState
    {
        None,
        Complete,
        Errors,
        Modified
    }
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
