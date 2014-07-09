using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Data
{
    /// <summary>
    /// Site/VisitType Level Config
    /// </summary>
    public class SiteVisitTypeScheduleConfig
    {
        public int SiteID { get; set; }
        public int VisitTypeID { get; set; }

        public bool RequiresVisitTime { get; set; }
        public bool RequiresMainRoom { get; set; }

    }
    /// <summary>
    /// VisitType Level Config
    /// </summary>
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