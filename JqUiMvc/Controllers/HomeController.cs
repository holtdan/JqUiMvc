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

            return View("VisitInfo",vm);
        }
        public ActionResult Navigate(string view)
        {
            var vm = new TaskViewModel(view);

            ViewBag.TaskSteps = Repository.GetSteps();
            ViewBag.CurrView = vm.CurrStep.View;

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
        //public ActionResult SaveAndContinue(TaskViewModel vm)
        //public ActionResult SaveAndContinue(string view, int? state)
        public ActionResult SaveAndContinue(SaveAndContinueParam sacP)
        {
            // if sacP.State != modified: return error/warning 

            Repository.Dbs[sacP.CurrView] = StepState.Complete;

            var gotoV = Repository.GetNextView(sacP.CurrView);

            //return RedirectToAction("Navigate", new { view = Repository.GetNextView(sacP.CurrView) });
            return Json(new { result = "Redirect", url = Url.Action("Navigate", "Home", new { area = "" }), view = gotoV });
        }
    }
}
