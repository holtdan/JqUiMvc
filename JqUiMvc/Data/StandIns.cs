using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Data
{
    public class Thing
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class Site : Thing
    {
        public IEnumerable<VisitType> VisitTypes { get; set; }
    }
    public class VisitType : Thing
    {

    }
    public class Room: Thing
    {
        public int SiteID { get; set; }
    }
    public class AgendaTopic : Thing
    {
        public int AgendaCategoryID { get; set; }
        public int DisplayOrder { get; set; }
        public int Duration { get; set; }
        public string Notes { get; set; }
        public bool IsMeal { get; set; }
        public bool AllowAM { get; set; }
        public bool IsRefSiteEquipment { get; set; }
    }
    public class AgendaCategory : Thing
    {
        public int DisplayOrder { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public bool IsMeal { get; set; }
        public bool AllowAM { get; set; }
        public bool IsProductModality { get; set; }

        public IEnumerable<AgendaTopic> Items { get; set; }
    }
    public class AgendaItem : Thing
    {
        public Room Room { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? AgendaTopicID { get; set; }
    }
}
