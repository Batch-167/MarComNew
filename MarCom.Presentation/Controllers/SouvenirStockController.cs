using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class SouvenirStockController : Controller
    {
        // GET: SouvenirStock
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", SouvenirStockRepo.Get());
        }

        public ActionResult AddItem()
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            SouvenirStockViewModel model = new SouvenirStockViewModel();
            return PartialView("_AddItem");
        }

        public ActionResult Add()
        {
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            return PartialView("_Add", new SouvenirStockViewModel());
        }

        [HttpPost]
        public ActionResult Add(SouvenirStockViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = SouvenirStockRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            SouvenirStockViewModel model = SouvenirStockRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(SouvenirStockViewModel model)
        {
            model.Update_By = User.Identity.Name;
            ResultResponse result = SouvenirStockRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            SouvenirStockViewModel model = SouvenirStockRepo.GetById(id);
            return PartialView("_Details", model);
        }
    }
}