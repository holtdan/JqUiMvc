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
}
