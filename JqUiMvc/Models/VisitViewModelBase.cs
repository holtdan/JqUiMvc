using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqUiMvc.Models
{
    public class VisitViewModelBase
    {
        public StepState StepState { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int VisitID { get; set; }
    }
}
