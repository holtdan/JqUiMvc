using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqUiMvc.Models
{
    public class VisitInfoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int VisitID { get; set; }

        [Required]
        [Display(Name = "Visit Name")]
        public String VisitName { get; set; }

        [Required]
        [Display(Name = "Visit Location")]
        public SelectList SiteList { get; set; }

        [Required]
        [Display(Name = "Visit Type")]
        public SelectList VisitType { get; set; }

        [Required]
        [Display(Name = "Is this an off-site visit?")]
        public bool? OffSite { get; set; }

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
        public SelectList AttendeeCount { get; set; }

        public VisitInfoViewModel() { }

        /// <summary>
        /// Prepare the view model for first-use
        /// </summary>
        public void Load()
        {
            this.VisitID = 0;
            //this.SiteList = BESession.GetSiteSelectList();
        }
    }
}
