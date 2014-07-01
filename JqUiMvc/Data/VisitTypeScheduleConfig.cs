using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Data
{
    public class SiteVisitTypeScheduleConfig
    {
        public int SiteID { get; set; }
        public int VisitTypeID { get; set; }

        public bool RequiresVisitTime { get; set; }
        public bool RequiresMainRoom { get; set; }

    }
    public class VisitTypeScheduleConfig
    {
        public int VisitTypeID { get; set; }
        public string Name { get; set; }    // would really get from EventType table
        
        public bool HideCustomers { get; set; }
        public bool HideOpportunities { get; set; }
        public bool HideReason { get; set; }
        public bool HideLogistics { get; set; }

        public bool IsVisitNameFreeform { get; set; }

        public bool RequiresOffSiteInfo { get; set; }
        public bool RequiresAcctMgr { get; set; }
        public bool RequiresMeetingOwner { get; set; }
    }
}