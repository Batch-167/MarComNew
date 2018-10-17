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
    [Authorize]
    public class MenuAccessController : Controller
    {
        // GET: MenuAccess
        public ActionResult Index()
        {
            return View();
        }

        //GET: List
        public ActionResult List()
        {
            return PartialView("_List", MenuAccessRepo.Get());
        }

        //GET: Create
        public ActionResult Create()
        {
            ViewBag.RoleMenu = new SelectList(RoleRepo.Get(), "Id", "Name");
            ViewBag.ListMenu = new SelectList(MenuRepo.Get(), "Id", "Name");
            MenuAccessViewModel model = new MenuAccessViewModel();
            return PartialView("_Create", model);
        }

        //POST: Create
        [HttpPost]
        public ActionResult Create(MenuAccessViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = MenuAccessRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //GET: Edit
        public ActionResult Edit(int id)
        {
            ViewBag.RoleMenu = new SelectList(RoleRepo.Get(), "Id", "Name");
            ViewBag.ListMenu = new SelectList(MenuRepo.Get(), "Id", "Name");
            ViewBag.IdMenu = new SelectList(MenuRepo.Get(), "Id", "Id");
            MenuAccessViewModel model = MenuAccessRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        //POST:Edit
        [HttpPost]
        public ActionResult Edit(MenuAccessViewModel model)
        {
            model.Create_By = User.Identity.Name;
            model.Update_By = User.Identity.Name;
            ResultResponse result = MenuAccessRepo.Update(model);
            return Json(new
            {
                success=result.Success,
                entity=model,
                message=result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //GET:Delete
        public ActionResult Delete(int id)
        {
            MenuAccessViewModel model = MenuAccessRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        //POST: Delete
        [HttpPost]
        public ActionResult Delete(MenuAccessViewModel model)
        {
            return RedirectToAction("Index");
        }

        //Delete Confirmation
        public ActionResult DeleteConfirm(int id)
        {
            bool result = MenuAccessRepo.Delete(id);
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