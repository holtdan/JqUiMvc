using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Models
{
    public class TaskStep
    {
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}