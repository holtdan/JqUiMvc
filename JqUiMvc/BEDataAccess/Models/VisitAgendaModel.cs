using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEDataAccess.Models
{
    public class VisitAgendaModel
    {
        public IList<AgendaItem> Items { get; set; }

        public VisitAgendaModel()
        {
            Items = new List<AgendaItem>();
        }
    }
}
