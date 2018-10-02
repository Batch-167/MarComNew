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
            return View();
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


        //DELETE
        //public ActionResult Delete(int id)
        //{

        //    RoleViewModel model = RoleRepo.GetById(id);
        //    return View("_Delete", model);
        //}

        ////POST
        //[HttpPost]
        //public ActionResult Delete(RoleViewModel model)
        //{
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult DeleteConfirm(int id)
        //{
        //    if (RoleRepo.Delete(id))
        //    {
        //        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //    }
        //}

    }
}