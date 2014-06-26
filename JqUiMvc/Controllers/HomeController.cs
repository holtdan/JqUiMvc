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
            return View("VisitInfo",new TaskViewModel());
        }

        public ActionResult Navigate(string page)
        {
            var vm = new TaskViewModel(page);

            return View(page,vm);
        }
        [HttpPost]
        public ActionResult SaveAndContinue(TaskViewModel vm)
        {
            // Save the Data!
            ViewBag.Message = "Your visit information was saved.";

            Repository.Dbs[vm.CurrStep.View] = StepState.Complete;

            var newVM = new TaskViewModel(vm.CurrStep.View);

            if (vm.CurrStep.Sequence == 0 && !newVM.HasDatabaseCore)
            {
                Repository.ID = DateTime.Now.Second;

                newVM.HasDatabaseCore = true;
            }
            newVM.GoToNextStep();

            return View(newVM.CurrStep.View,newVM);
        }
    }
}
