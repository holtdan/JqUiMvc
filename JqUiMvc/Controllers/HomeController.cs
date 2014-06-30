using JqUiMvc.Data;
using JqUiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqUiMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new TaskViewModel();

            Repository.Dbs.Clear();

            ViewBag.TaskSteps = Repository.GetSteps();
            ViewBag.CurrView = vm.CurrStep.View;
            ViewBag.TaskTitle = "New Request";

            return View("VisitInfo",vm);
        }
        public ActionResult Navigate(string view)
        {
            var vm = new TaskViewModel(view);

            ViewBag.TaskSteps = Repository.GetSteps();
            ViewBag.CurrView = vm.CurrStep.View;
            ViewBag.TaskTitle = vm.HasDatabaseCore ? "Visit Name!" : "New Request";

            return View(view,vm);
        }
        [HttpPost]
        public ActionResult UpdateViewData(string view, int state)
        {
            var stepState = (StepState)state;

            Repository.Dbs[view] = stepState;

            return Json(new { 
                success = true,
                message = string.Format("{0} updated to {1}.", view, stepState)
            });
        }
        [HttpPost]
        public ActionResult SaveAndContinue(SaveAndContinueParam sacP)
        {
            // if sacP.State != modified: return error/warning 

            Repository.Dbs[sacP.CurrView] = StepState.Complete;

            var gotoV = !string.IsNullOrEmpty(sacP.GoToView)
                ? sacP.GoToView //TODO: validate
                : Repository.GetNextView(sacP.CurrView);

            //return RedirectToAction("Navigate", new { view = Repository.GetNextView(sacP.CurrView) });
            return Json(new { result = "Redirect", url = Url.Action("Navigate", "Home", new { area = "" }), view = gotoV });
        }
        [HttpPost]
        public ActionResult DiscardAndContinue(SaveAndContinueParam sacP)
        {
            // if sacP.State != modified: return error/warning 

            Repository.Dbs[sacP.CurrView] = StepState.None; // or could be StepState.Complete

            var gotoV = !string.IsNullOrEmpty(sacP.GoToView)
                ? sacP.GoToView //TODO: validate
                : Repository.GetNextView(sacP.CurrView);

            //return RedirectToAction("Navigate", new { view = Repository.GetNextView(sacP.CurrView) });
            return Json(new { result = "Redirect", url = Url.Action("Navigate", "Home", new { area = "" }), view = gotoV });
        }
    }
}
