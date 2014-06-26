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
            return View(page,new TaskViewModel(page));
        }
        [HttpPost]
        public ActionResult SaveAndContinue(TaskViewModel vm)
        {
            var newVM = new TaskViewModel(vm.CurrStep.View);

            // Save the Data!
            ViewBag.Message = "Your visit information was saved.";

            if (vm.CurrStep.Sequence == 0)
            {
                newVM.HasDatabaseCore = true;
            }
            newVM.GoToNextStep();

            return View(newVM.CurrStep.View,newVM);
        }
    }
}
