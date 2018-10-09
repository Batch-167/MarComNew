using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class UnitController : Controller
    {
        // GET: Unit
        public ActionResult Index()
        {
            ViewBag.Unit1 = new SelectList(UnitRepo.Get(), "Code", "Code");
            ViewBag.Unit2 = new SelectList(UnitRepo.Get(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(UnitViewModel model)
        {
            return PartialView("_List", UnitRepo.Filter(model));
        }

        public ActionResult List()
        {
            return PartialView("_List", UnitRepo.Get());
        }

        public ActionResult Add()
        {
            return View("_Add", new UnitViewModel());
        }

        [HttpPost]
        public ActionResult Add(UnitViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = UnitRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            UnitViewModel model = UnitRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(UnitViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = UnitRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            UnitViewModel model = UnitRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(UnitViewModel model)
        {
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = UnitRepo.Delete(id);
            if (UnitRepo.Delete(id))
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "Delete Success"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "Delete Failed"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int id)
        {
            UnitViewModel model = UnitRepo.GetById(id);
            return PartialView("_Details", model);
        }
    }
}