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
    }
    public class AgendaCategory : Thing
    {
        public IEnumerable<AgendaItem> Items { get; set; }
    }
}
