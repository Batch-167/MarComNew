using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MarCom.Presentation.Controllers
{
    //[Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.CompanyName = new SelectList(CompanyRepo.Get(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(EmployeeViewModel model)
        {
            return PartialView("_List", EmployeeRepo.Filter(model));
        }


        // GET: List
        public ActionResult List()
        {
            
            return PartialView("_List", EmployeeRepo.Get());
        }

        public ActionResult View(int id)
        {
            EmployeeViewModel model = EmployeeRepo.GetById(id);
            return PartialView("_View", model);
        }

        // GET : Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(CompanyRepo.Get(), "id","Name");
            return PartialView("_Create", new EmployeeViewModel());
        }

        //POST : Create
        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {
            //model.Create_By = User.Identity.Name;
            ResultResponse result = EmployeeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //GET : Edit
        public ActionResult Edit(int id)
        {
            ViewBag.Company = new SelectList(CompanyRepo.Get(), "id", "Name");
            EmployeeViewModel model = EmployeeRepo.GetById(id);
            return PartialView("_Edit", model);
        }
        //POST: edit
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel model)
        {
            ResultResponse result = EmployeeRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //GET: Delete
        public ActionResult Delete(int id)
        {
            EmployeeViewModel model = EmployeeRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        //POST: Delete
        [HttpPost]
        public ActionResult Delete(EmployeeViewModel model)
        {
            return RedirectToAction("Index");
        }

        //Delete Confirmation
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = EmployeeRepo.Delete(id);
            
            if (result)
            {
                return Json(new
                {
                    success = result,
                    entity = "null",
                    message = "Delete Success"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = result,
                    entity = "null",
                    message = "Delete Failed"
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}