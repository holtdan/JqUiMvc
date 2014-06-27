﻿using JqUiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Data
{
    public class Repository : IDisposable
    {
        public static int? ID;
        public static Dictionary<string, StepState> Dbs = new Dictionary<string, StepState>();
        //public static bool HasDatabaseCore { get { return ID != null; } }

        public static IEnumerable<TaskStep> GetSteps()
        {
            if (Dbs.Count() == 0)
                foreach (var s in new string[] {"VisitInfo","Owner","Customers","Opportunities","Attendees","Reason","Logistics","Documents"})
                    Dbs[s] = StepState.None;

            var steps = new TaskStep[]{
                new TaskStep{Sequence=0,Name="Info",View="VisitInfo"},
                new TaskStep{Sequence=1,Name="Owner",View="Owner"},
                new TaskStep{Sequence=2,Name="Customers",View="Customers"},
                new TaskStep{Sequence=3,Name="Opportunities",View="Opportunities"},
                new TaskStep{Sequence=4,Name="Attendees",View="Attendees"},
                new TaskStep{Sequence=5,Name="Reason",View="Reason"},
                new TaskStep{Sequence=6,Name="Logistics",View="Logistics"},
                new TaskStep{Sequence=7,Name="Documents",View="Documents"},
            };

            foreach (var s in steps) s.State = Dbs[s.View];

            return steps;
        }
        public static string GetNextView(string afterThis1)
        {
            var steps = GetSteps();

            var currSeqNum = steps.Where(s=>s.View == afterThis1).Single().Sequence;
            var nextSeq = steps.Where(s => !s.Hidden && s.Sequence > currSeqNum).OrderBy(s => s.Sequence).FirstOrDefault();

            return (nextSeq == null) ? steps.First().View : nextSeq.View;
        }
        public void Dispose()
        {
        }
    }
}
