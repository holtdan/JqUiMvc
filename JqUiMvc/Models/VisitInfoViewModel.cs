using JqUiMvc.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEDataAccess;

namespace JqUiMvc.Models
{
    public class VisitInfoViewModel : VisitViewModelBase
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Location")]
        public int SiteID { get; set; }
        public SITES Site { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Visit Type")]
        public int VisitTypeID { get; set; }

        [Required]
        [Display(Name = "Visit Name")]
        public string VisitName { get; set; }
        //[Required]
        //[Display(Name = "Location")]
        //public string Location { get; set; }
        //[Required]
        //[Display(Name = "Visit Type")]
        //public string VisitType { get; set; }

        [Required]
        [Display(Name = "Is this an off-site visit?")]
        public bool IsOffSite { get; set; }

        public enum VLength
        {
            Hourly,
            Full,
            Multi
        }
        [Required]
        [Display(Name = "Visit Length")]
        public VLength? VisitLength { get; set; }

        [Required]
        [Display(Name = "Estimated # of Attendees")]
        public int? NumAttendees { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Room ID")]
        public int? RoomID { get; set; }

        [Display(Name = "# Attendees")]
        public SelectList AttendeeSelList { get; set; }

        [Display(Name = "Visit Location")]
        public SelectList SiteSelList { get; set; }

        [Display(Name = "Visit Type")]
        public SelectList VisitTypeSelList { get; set; }

        public VisitInfoViewModel() { }

        /// <summary>
        /// Prepare the view model for new Visit
        /// </summary>
        public void Load()
        {
            this.VisitID = 0;
            this.VisitName = "New Request";
            //var e = new BEDataAccess.Engine();
            using (var dc = new BEDataContext())
            {
                this.SiteID = Repository.DefaultSiteID; // this would be user's default site

                var sites = dc.Sites.ToList();
                var visTypes = dc.SiteEvents.Where(r => r.SiteID == this.SiteID).Select(s => s.VisitType).ToList();

                this.SiteSelList = new SelectList(sites, "SITEID", "SITENAME", this.SiteID);
                this.VisitTypeSelList = new SelectList(visTypes, "EventTypeID", "EventType1", this.VisitTypeID);

                this.Site = sites.Where(s => s.SITEID == this.SiteID).Single();
                this.VisitTypeID = visTypes.First().EventTypeID;
            }
#if(DEBUG)
            this.VisitLength = VLength.Full;
            this.NumAttendees = 5;
#endif
        }
        /// <summary>
        /// Prepare the view model from existing Visit
        /// </summary>
        /// <param name="visitID"></param>
        public void Load(int visitID)
        {
            var visMod = Repository.VisitModel; // here's where we'd read the db

            this.VisitID = visMod.VisitID;
            this.VisitName = visMod.VisitName;
            this.VisitTypeID = visMod.VisitTypeID;
            this.SiteID = visMod.SiteID;
            this.IsOffSite = visMod.IsOffSite;
            this.StartDate = visMod.StartDate;
            this.EndDate = visMod.EndDate;

            this.VisitLength =
                visMod.IsHourly ? VLength.Hourly :
                visMod.IsFullDay ? VLength.Full : VLength.Multi;

            Fluff();
        }
        public VisitInfoViewModel Fluff()
        {
            using (var dc = new BEDataContext())
            {
                var sites = dc.Sites;
                var visTypes = dc.SiteEvents.Where(r => r.SiteID == this.SiteID).Select(s => s.VisitType);

                this.SiteSelList = new SelectList(sites, "SITEID", "SITENAME", this.SiteID);
                this.VisitTypeSelList = new SelectList(visTypes, "EventTypeID", "EventTypeID1", this.VisitTypeID);
            }

            return this;
        }
    }
}
