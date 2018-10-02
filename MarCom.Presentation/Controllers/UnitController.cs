using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    //[Authorize]
    public class UnitController : Controller
    {
        // GET: Unit
        public ActionResult Index()
        {
            return View();
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
            if (UnitRepo.Delete(id))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int id)
        {
            UnitViewModel model = UnitRepo.GetById(id);
            return PartialView("_Details", model);
        }
    }
}