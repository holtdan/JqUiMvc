using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    public class TaskViewModel
    {
        public IEnumerable<TaskStep> Steps { get; set; }
    }
}