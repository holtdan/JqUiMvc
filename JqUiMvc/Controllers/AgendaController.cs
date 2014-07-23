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

            //var agIt = new AgendaItem
            //{
            //    Room = Repository.GetRooms().Where(r=>r.SiteID == siteID).First(),
            //    Start = Repository.VisitModel.StartDate,
            //    End = Repository.VisitModel.EndDate,
            //};

            //Repository.VisitModel.Agenda.Items.Add(agIt);

            ViewBag.AgendaCats = Repository.GetAgendaTopicCategories();
            ViewBag.Rooms = Repository.GetRooms().Where(r => r.SiteID == siteID);

            return View(Repository.VisitModel);
        }
        [HttpGet]
        public ActionResult GetRoom(int roomID)
        {
            var room = Repository.GetRooms().Where(r => r.ID == roomID).Single();

            return PartialView("_AgendaRoom", room);
        }
        [HttpGet]
        public ActionResult GetAgendaItem(int topicID, string instanceID)
        {
            var agIt = new AgendaItem
            {
                ID = topicID,
                Name = "TODO: Name for #" + topicID.ToString(),
                InstanceID = instanceID
            };

            //var room = Repository.GetRooms().Where(r => r.ID == roomID).Single();

            return PartialView("_AgendaItem", agIt);
        }
	}
}
