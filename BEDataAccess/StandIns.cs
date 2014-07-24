using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEDataAccess
{
    public class SITES
    {
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string DateFormat { get; set; }
        public string SiteType { get; set; }
        public string DefaultStartTime_FullDay { get; set; }
        public string DefaultEndTime_FullDay { get; set; }
        public string DefaultStartTime_AM { get; set; }
        public string DefaultEndTime_AM { get; set; }
        public string DefaultStartTime_PM { get; set; }
        public string DefaultEndTime_PM { get; set; }
        public float MinimumVisitLength { get; set; }
        public float MaximumVisitLength { get; set; }
        public int MaxVisitsPerDay { get; set; }

        public virtual ICollection<ROOMS> Rooms { get; set; }

    }
    public partial class EventType
    {
        public int EventTypeID { get; set; }
        public string BriefingOrEvent { get; set; }
        public string EventType1 { get; set; }
        public Nullable<short> Event { get; set; }
        public Nullable<short> RoomRequired { get; set; }
        public short IncludeInExecRpts { get; set; }
        public short CountsTowardTarget { get; set; }
        public short ActiveFlag { get; set; }
        public Nullable<System.DateTime> DateLastUpdated { get; set; }
        public Nullable<short> HostBrieferRequired { get; set; }
        public string EventTypeDisplayBGColor { get; set; }
        public short TopicsRequired { get; set; }
        public string EventTypeDescription { get; set; }
        public short Required_OpportunityID { get; set; }
        public short Required_OpportunityName { get; set; }
        public short Required_OpportunityStageNum { get; set; }
        public short Required_PendingCloseDate { get; set; }
        public short Required_AcctPotential { get; set; }
        public short Required_OpportunityForSale { get; set; }
        public short Required_AnnualRevFromClient { get; set; }
        public string Label_AnnualRevFromClient { get; set; }
        public short Required_OpportunityCompetitors { get; set; }
        public short Required_OpportunityRevenueShare { get; set; }
        public short FollowupBrieferRequired { get; set; }
        public int DisplayOrder { get; set; }
        public short InternalOnly { get; set; }
    }
    public partial class ROOMS
    {
        public int ROOMID { get; set; }
        public Nullable<int> SITEID { get; set; }
        public string ROOMNAME { get; set; }
        public string LONGROOMNAME { get; set; }
        public Nullable<int> OCCUPANCY { get; set; }
        public short DefaultBooking { get; set; }
        public Nullable<bool> ExclusiveRoom { get; set; }
        public string MEMOFIELD { get; set; }
        public short ACTIVEFLAG { get; set; }
        public System.DateTime DateLastUpdated { get; set; }
        public short Sharable { get; set; }
        public string ReportColor { get; set; }

        // Navigation properties 
        public virtual SITES Site { get; set; } 
    }
}
