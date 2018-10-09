using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            ViewBag.Role1 = new SelectList(RoleRepo.Get(), "Code", "Code");
            ViewBag.Role2 = new SelectList(RoleRepo.Get(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(RoleViewModel model)
        {
            return PartialView("_List", RoleRepo.Filter(model));
        }

        public ActionResult List()
        {
            return PartialView("_List", RoleRepo.Get());
        }


        //GET : New Role
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(RoleRepo.Get(), "Id", "Name");
            return PartialView("_Create", new RoleViewModel());
        }

        //POS
        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            ResultResponse result = RoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult View(int id)
        {
            RoleViewModel model = RoleRepo.GetById(id);
            return PartialView("_View", model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Role = new SelectList(RoleRepo.Get(), "Id", "Name");
            RoleViewModel model = RoleRepo.GetById(id);
            return View("_Edit", model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            ResultResponse result = RoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id)
        {

            RoleViewModel model = RoleRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        //POST
        [HttpPost]
        public ActionResult Delete(RoleViewModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = RoleRepo.Delete(id);

            if (result)
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
                    message = "delete failed"
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}