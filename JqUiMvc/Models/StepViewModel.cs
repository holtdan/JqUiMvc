using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    /// <summary>
    /// Still in infancy: not used yet.
    /// Will/Can each ViewModel inherit from this?
    /// Or should it be an interface?
    /// </summary>
    public class StepViewModel
    {
        public StepState State { get; set; }
    }
}
