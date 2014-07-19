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
    public class AgendaItem : Thing
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

        public IEnumerable<AgendaItem> Items { get; set; }
    }
}
