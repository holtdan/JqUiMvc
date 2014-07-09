using JqUiMvc.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqUiMvc.Models
{
    public class VisitInfoViewModel : VisitViewModelBase
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Location")]
        public int SiteID { get; set; }

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

            using ( var dc = new Repository())
            {
                this.SiteID = Repository.DefaultSiteID; // this would be user's default site

                var sites = Repository.GetSites();
                var visTypes = Repository.GetVisitTypes(this.SiteID);

                this.VisitTypeID = visTypes.First().ID;

                this.SiteSelList = new SelectList(sites, "ID", "Name", this.SiteID);
                this.VisitTypeSelList = new SelectList(visTypes, "ID", "Name", this.VisitTypeID);
            }
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
            using (var dc = new Repository())
            {
                var sites = Repository.GetSites();
                var visTypes = Repository.GetVisitTypes(this.SiteID);

                this.SiteSelList = new SelectList(sites, "ID", "Name", this.SiteID);
                this.VisitTypeSelList = new SelectList(visTypes, "ID", "Name", this.VisitTypeID);
            }

            return this;
        }
    }
}
