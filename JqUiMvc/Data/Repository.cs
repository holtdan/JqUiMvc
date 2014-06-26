using JqUiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Data
{
    public class Repository : IDisposable
    {
        public static IEnumerable<TaskStep> GetSteps()
        {
            return new TaskStep[]{
                new TaskStep{Sequence=0,Name="Info",View="VisitInfo"},
                new TaskStep{Sequence=1,Name="Owner",View="Owner"},
                new TaskStep{Sequence=2,Name="Customers",View="Customers"},
                new TaskStep{Sequence=3,Name="Opportunities",View="Opportunities"},
                new TaskStep{Sequence=4,Name="Attendees",View="Attendees"},
                new TaskStep{Sequence=5,Name="Reason",View="Reason"},
                new TaskStep{Sequence=6,Name="Logistics",View="Logistics"},
                new TaskStep{Sequence=7,Name="Documents",View="Documents"},
            };
        }

        public void Dispose()
        {
        }
    }
}
