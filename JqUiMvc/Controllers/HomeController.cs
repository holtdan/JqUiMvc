using JqUiMvc.Data;
using JqUiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEDataAccess.Models;

namespace JqUiMvc.Controllers
{
    public class HomeController : Controller
    {
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult == null) // when/why does this happen?
                return;

            var vm = viewResult.Model as VisitViewModelBase;

            base.OnResultExecuting(filterContext);
        }
        /// <summary>
        /// Start up.
        /// </summary>
        public ActionResult Index()
        {
            Repository.Reset();

            var vm = new ScheduleVisitTaskViewModel();

            return View("VisitInfo", GetView ( vm ));
        }
        public ActionResult GetCalendar(VisitInfoViewModel vm)
        {
	        return PartialView("_VisitSelectDateFull", vm);
        }
        /// <summary>
        /// Go to a particular view.
        /// The controller doesn't care about the state of the on-screen data at this point. 
        /// It's assumed navigating away is OK because the caller says it is.
        /// </summary>
        public ActionResult Navigate(string view)
        {
            int? visitID = TempData["VisitID"] != null ? (int)TempData["VisitID"] : (int?)null;

            var vm = new ScheduleVisitTaskViewModel(view);

            return View(view, GetView ( vm, visitID ));
        }
        VisitViewModelBase GetView(ScheduleVisitTaskViewModel svvm, int? visitID = null)
        {
            var step = svvm.CurrStep;

            //if (step.View != "VisitInfo" && visitID == null)
            //    throw new ArgumentException("Non-VisitInfo view requested with no Visit ID given.");

            //
            // We ALWAYS have a VisitInfoViewModel handy
            //
            var visitInfoVM = new VisitInfoViewModel { StepState = step.State };

            if (visitID != null)
            {
                visitInfoVM.Load(visitID.Value);
            }
            else
            {
                visitInfoVM.Load();
            }

            ViewBag.VisitInfoViewModel = visitInfoVM;
            ViewBag.TaskViewModel = svvm;

            switch (step.View)
            {
                case "VisitInfo":
                    {
                        return visitInfoVM;
                    }
                default:
                    {
                        return new VisitViewModelBase() { StepState = step.State, VisitID = visitID??0 };
                    }
            }
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
        [HttpPost]
        public ActionResult VisitInfo_When_SAC(VisitInfoViewModel vm)
        {
            var svvm = new ScheduleVisitTaskViewModel("VisitInfo");

            ViewBag.VisitInfoViewModel = vm.Fluff();
            ViewBag.TaskViewModel = svvm;

            return View(svvm.CurrStep.View, vm);
        }
        [HttpPost]
        public ActionResult VisitInfo_SAC(VisitInfoViewModel vm)
        {
            var svvm = new ScheduleVisitTaskViewModel("VisitInfo");

            //if (vm.VisitLength == null)
            //{
            //    ModelState.AddModelError("VisitLength", "Please specify a value");
            //}
            //if (vm.NumAttendees == null)
            //{
            //    ModelState.AddModelError("NumAttendees", "Please specify a value");
            //}
            if ( ModelState.IsValid )
            {
                if (vm.StartDate == null) // Need to get Time before proceeding
                {
                    var gotoView = "";

                    switch (vm.VisitLength)
                    {
                        case VisitInfoViewModel.VLength.Full: gotoView = "VisitInfoFullDay";
                            break;
                        case VisitInfoViewModel.VLength.Hourly: gotoView = "VisitInfoHourly";
                            break;
                        case VisitInfoViewModel.VLength.Multi: gotoView = "VisitInfoMultiDay";
                            break;
                    }

                    return View(gotoView, GetView(svvm));
                }
                // else user is back on main VisitInfo view and it's time to really Save

                if (Repository.VisitModel == null)
                {
                    Repository.VisitModel = new VisitModel
                    {
                        VisitID = 99,
                        VisitName = vm.VisitName,
                        VisitTypeID = vm.VisitTypeID,
                        SiteID = vm.SiteID,
                        IsOffSite = vm.IsOffSite,
                        StartDate = vm.StartDate.Value,
                        EndDate = vm.EndDate.Value
                    };
                    Repository.Dbs["VisitInfo"] = StepState.Complete;
                }
                else
                {
                    // store updated info?
                }

                TempData["VisitID"] = Repository.VisitModel.VisitID;

                return RedirectToAction("Navigate", new { view = svvm.GetNextView(svvm.CurrStep.View) });
            }
            else // VisitInfo view has errors that user must correct
            {
                return View("VisitInfo", vm.Fluff());
            }
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
