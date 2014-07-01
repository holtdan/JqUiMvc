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
        /// <summary>
        /// Start up.
        /// </summary>
        public ActionResult Index()
        {
            Repository.Dbs.Clear();

            var vm = new ScheduleVisitTaskViewModel();

            ViewBag.TaskTitle = "New Request";
            ViewBag.TaskViewModel = vm;

            return View("VisitInfo", GetView ( vm.CurrStep ));
        }
        VisitViewModelBase GetView(TaskStep step)
        {
            switch (step.View)
            {
                case "VisitInfo": 
                    return new VisitInfoViewModel { StepState = step.State, VisitName = "My Name" };
                default: 
                    return new VisitViewModelBase() {StepState = step.State};
            }
        }
        /// <summary>
        /// Go to a particular view.
        /// The controller doesn't care about the state of the on-screen data at this point. 
        /// It's assumed navigating away is OK because the caller says it is.
        /// </summary>
        public ActionResult Navigate(string view)
        {
            var vm = new ScheduleVisitTaskViewModel(view);

            ViewBag.TaskTitle = vm.HasDatabaseCore ? "Visit Name!" : "New Request";
            ViewBag.TaskViewModel = vm;

            return View(view, GetView ( vm.CurrStep ));
        }
        /// <summary>
        /// So far, this is just for development. This will no doubt get bigger and more complicated.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateViewData(string view, int state)
        {
            var stepState = (StepState)state;

            Repository.Dbs[view] = stepState;

            return Json(new
            {
                success = true,
                message = string.Format("{0} updated to {1}.", view, stepState)
            });
        }
        /// <summary>
        /// Called when user is saving changes and moving on to other step.
        /// If the GoToView field is null in the param class, the next available
        /// view is called up.
        /// </summary>
        [HttpPost]
        public ActionResult SaveAndContinue(SaveAndContinueParam sacP)
        {
            // if sacP.State != modified: return error/warning 

            var vm = new ScheduleVisitTaskViewModel(sacP.CurrView);

            Repository.Dbs[sacP.CurrView] = StepState.Complete;

            var gotoV = !string.IsNullOrEmpty(sacP.GoToView)
                ? sacP.GoToView //TODO: validate
                : vm.GetNextView(sacP.CurrView);

            //return RedirectToAction("Navigate", new { view = Repository.GetNextView(sacP.CurrView) });
            return Json(new { result = "Redirect", url = Url.Action("Navigate", "Home", new { area = "" }), view = gotoV });
        }
        /// <summary>
        /// Used when user has made changes but chooses to navigate away and lose those changes.
        /// This _might_ not be necessary: simply calling Navigate might be OK. We'll see when it gets real-er.
        /// </summary>
        [HttpPost]
        public ActionResult DiscardAndContinue(SaveAndContinueParam sacP)
        {
            // if sacP.State != modified: return error/warning 

            var vm = new ScheduleVisitTaskViewModel(sacP.CurrView);

            Repository.Dbs[sacP.CurrView] = StepState.None; // or could be StepState.Complete

            var gotoV = !string.IsNullOrEmpty(sacP.GoToView)
                ? sacP.GoToView //TODO: validate
                : vm.GetNextView(sacP.CurrView);

            //return RedirectToAction("Navigate", new { view = Repository.GetNextView(sacP.CurrView) });
            return Json(new { result = "Redirect", url = Url.Action("Navigate", "Home", new { area = "" }), view = gotoV });
        }
    }
}
