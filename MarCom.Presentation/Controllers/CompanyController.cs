using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", CompanyRepo.Get());
        }

        //GET
        public ActionResult Create()
        {
            return PartialView("_Create", new CompanyViewModel());
        }

        //POST
        [HttpPost]
        public ActionResult Create(CompanyViewModel model)
        {
            ResultResponse result = CompanyRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //GET

        public ActionResult Edit(int id)
        {
            CompanyViewModel model = CompanyRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit(CompanyViewModel model)
        {
            ResultResponse result = CompanyRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int id)
        {
            CompanyViewModel model = CompanyRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CompanyViewModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            if (CompanyRepo.Delete(id))
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
            CompanyViewModel model = CompanyRepo.GetById(id);
            return PartialView("_Details", model);
        }

    }
}