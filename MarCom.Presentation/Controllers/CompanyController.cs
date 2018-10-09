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
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            ViewBag.Company = new SelectList(CompanyRepo.Get(), "Code", "Code");
            ViewBag.Company1 = new SelectList(CompanyRepo.Get(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(CompanyViewModel model)
        {
            return PartialView("_List", CompanyRepo.Filter(model));
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
            model.Create_By = User.Identity.Name;
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
            bool result = ProductRepo.Delete(id);
            if (CompanyRepo.Delete(id))
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "delete success"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "delete fail"
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Details(int id)
        {
            CompanyViewModel model = CompanyRepo.GetById(id);
            return PartialView("_Details", model);
        }

    }
}