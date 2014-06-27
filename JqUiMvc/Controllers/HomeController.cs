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
            ViewBag.TaskSteps = Repository.GetSteps();

            Repository.Dbs.Clear();

            return View("VisitInfo",new TaskViewModel());
        }
        [HttpPost]
        public ActionResult Index(TaskViewModel vm)
        {
            ViewBag.TaskSteps = Repository.GetSteps();

            return View(vm.CurrStep.View, new TaskViewModel());
        }
        public ActionResult Navigate(string view)
        {
            if ( view == null)
                view = TempData["nextView"] as string;

            var vm = new TaskViewModel(view);

            ViewBag.TaskSteps = Repository.GetSteps();

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
        public ActionResult SaveAndContinue(TaskViewModel vm)
        {
            var view = vm.CurrStep.View;

            Repository.Dbs[view] = StepState.Complete;
            
            return RedirectToAction("Navigate", new { view = Repository.GetNextView(view) });
        }
    }
}
