using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEDataAccess
{
    
    public class SITES
    {
        [Key]
        public int SITEID { get; set; }
        public string SITENAME { get; set; }
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

        // Navigation properties
        //public virtual ICollection<EventType> VisitTypes { get; set; }
        public virtual ICollection<ROOMS> Rooms { get; set; }
    }
    public partial class EventType
    {
        [Key]
        public int EventTypeID { get; set; }
        public string BriefingOrEvent { get; set; }
        [Column("EventType")]
        public string EventType1 { get; set; }
        public Nullable<short> Event { get; set; }
        public Nullable<short> RoomRequired { get; set; }
        public short IncludeInExecRpts { get; set; }
        public short CountsTowardTarget { get; set; }
        public short ActiveFlag { get; set; }
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
        [Key]
        public int ROOMID { get; set; }
        public Nullable<int> SITEID { get; set; }
        public string ROOMNAME { get; set; }
        public string LONGROOMNAME { get; set; }
        public Nullable<int> OCCUPANCY { get; set; }
        public short DefaultBooking { get; set; }
        public Nullable<bool> ExclusiveRoom { get; set; }
        public string MEMOFIELD { get; set; }
        public short ACTIVEFLAG { get; set; }
        public short Sharable { get; set; }
        public string ReportColor { get; set; }

        // Navigation properties
        public virtual SITES Site { get; set; }
    }
    public partial class SiteEvents
    {
        [Key]
        [Column(Order = 1)]
        public int SiteID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int EventTypeID { get; set; }
        public short ActiveFlag { get; set; }
        public int MinSchedulingDays { get; set; }
        public short VisitApprovalRequired { get; set; }
        public short AMCanSchedule { get; set; }
        public short VisitDocumentsRequired { get; set; }

        // Navigation properties 
        public virtual SITES Site { get; set; }
        public virtual EventType VisitType { get; set; }
    }
    public partial class VISITS
    {
        [Key]
        public int VISITID { get; set; }
        public Nullable<int> VISITTYPEID { get; set; }
        public Nullable<short> BriefingTypeID { get; set; }
        public int VISITSTATUSID { get; set; }
        public Nullable<int> SITEID { get; set; }
        public Nullable<int> COORDID { get; set; }
        public Nullable<int> ACCTMGRID { get; set; }
        public Nullable<int> REQUESTERID { get; set; }
        public int SEGMENTID { get; set; }
        public int REGIONID { get; set; }
        public Nullable<int> REASONID { get; set; }
        public Nullable<int> EventTypeID { get; set; }
        public Nullable<System.DateTime> VISITSTART { get; set; }
        public Nullable<System.DateTime> VISITEND { get; set; }

        // Navigation properties 
        public virtual SITES Site { get; set; }
        public virtual EventType VisitType { get; set; }
    }
    public partial class USERS
    {
        [Key]
        public int USERID { get; set; }
        public string LNAME { get; set; }
        public string FNAME { get; set; }
        public int ACCESSLEVEL { get; set; }
        public short Sub_AccessLevel { get; set; }
        public int SITEID { get; set; }
        public short ACTIVEFLAG { get; set; }
        public string USERNAME { get; set; }

        // Navigation properties 
        public virtual SITES Site { get; set; }
    }

    public class Thing
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class AgendaItem : Thing
    {
        public ROOMS Room { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? AgendaTopicID { get; set; }

        public string InstanceID { get; set; }
    }
}
