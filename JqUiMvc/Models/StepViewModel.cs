using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    public class StepViewModel
    {
        public string MyProperty { get; set; }

        public TaskStep StepInfo { get; set; }
        public StepViewModel(TaskStep step)
        {
            StepInfo = step;
        }
    }
}
