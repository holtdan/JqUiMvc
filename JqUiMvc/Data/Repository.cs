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
                new TaskStep{Sequence=0,Name="Get Initial Data"},
                new TaskStep{Sequence=1,Name="And Then..."},
                new TaskStep{Sequence=2,Name="Finally"},
            };
        }

        public void Dispose()
        {
        }
    }
}
