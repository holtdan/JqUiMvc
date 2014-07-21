using JqUiMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqUiMvc.Controllers
{
    public class AgendaController : Controller
    {
        //
        // GET: /Agenda/
        public ActionResult Index()
        {
            var siteID = Repository.DefaultSiteID;

            Repository.VisitModel = new VisitModel
            {
                VisitID = 99,
                VisitName = "Visit Agenda",
                VisitTypeID = Repository.GetVisitTypes(siteID).First().ID,
                SiteID = siteID,
                //IsOffSite = vm.IsOffSite,
                StartDate = DateTime.Parse("2014-07-28 08:00"),
                EndDate = DateTime.Parse("2014-07-28 14:00"),
            };

            Repository.VisitModel.Agenda = new VisitAgendaModel();

            var agIt = new AgendaItem
            {
                Room = Repository.GetRooms(siteID).First(),
                Start = Repository.VisitModel.StartDate,
                End = Repository.VisitModel.EndDate,
            };

            ViewBag.AgendaCats = Repository.GetAgendaTopicCategories();
            ViewBag.Rooms = Repository.GetRooms(siteID);

            return View();
        }
	}
}