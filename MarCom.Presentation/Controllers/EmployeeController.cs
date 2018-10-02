using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
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
            ViewBag.Company = new SelectList(EmployeeRepo.Get(), "id","Name");
            return PartialView("_Create", new EmployeeViewModel());
        }

        //POST : Create
        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {
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
            EmployeeViewModel model = EmployeeRepo.GetById(id);
            return View("_Edit", model);
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
            if (EmployeeRepo.Delete(id))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}