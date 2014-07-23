using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqUiMvc.Data
{
    public class VisitModel
    {
        public int VisitID { get; set; }
        public string VisitName { get; set; }
        public int SiteID { get; set; }
        public int VisitTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOffSite { get; set; }
        public int NumAttendees { get; set; }
        public VisitAgendaModel Agenda { get; set; }

        public IEnumerable<Room> GetRooms(DateTime? forDay = null)
        {
            
            if (Agenda.Items.Count() == 0)
                return new Room[] { Repository.GetRooms().Where(r=>r.SiteID == this.SiteID).FirstOrDefault() };
            else
                return Agenda.Items.Where(a => a.Start.Date == (forDay ?? this.StartDate.Date)).Select(a => a.Room);
        }
        /// <summary>
        /// Infers from StartDate/EndDate values
        /// TODO: Finish! Hours calc not right!
        /// </summary>
        public bool IsHourly
        {
            get
            {
                return StartDate.Date == EndDate.Date && 
                    (EndDate - StartDate).Hours < 8;
            }
        }
        /// <summary>
        /// Infers from StartDate/EndDate values
        /// TODO: Finish! Hours calc not right!
        /// </summary>
        public bool IsFullDay
        {
            get
            {
                return StartDate.Date == EndDate.Date && 
                    (EndDate - StartDate).Hours >= 8;
            }
        }
        /// <summary>
        /// Infers from StartDate/EndDate values
        /// </summary>
        public bool IsMultiDay
        {
            get
            {
                return StartDate.Date != EndDate.Date;
            }
        }
    }
}
