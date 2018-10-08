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
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            ViewBag.Menu1 = new SelectList(MenuRepo.Get(), "Code", "Code");
            ViewBag.Menu2 = new SelectList(MenuRepo.Get(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(MenuViewModel model)
        {
            return PartialView("_List", MenuRepo.Filter(model));
        }

        public ActionResult List()
        {
            return PartialView("_List", MenuRepo.Get());
        }

        public ActionResult Create()
        {
            ViewBag.Menu = new SelectList(MenuRepo.GetMenu(), "Id", "Controller");
            return PartialView("_Create", new MenuViewModel());
        }

        //POS        
        [HttpPost]
        public ActionResult Create(MenuViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = MenuRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Menu = new SelectList(MenuRepo.GetMenu(), "Id", "Controller");
            MenuViewModel model = MenuRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        //POST        
        [HttpPost]
        public ActionResult Edit(MenuViewModel model)
        {
            model.Update_By = User.Identity.Name;
            ResultResponse result = MenuRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show(int id)
        {
            MenuViewModel model = MenuRepo.GetById(id);
            return PartialView("_Show", model);
        }

        public ActionResult Delete(int id)
        {

            //ViewBag.Unit = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            MenuViewModel model = MenuRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        //POST
        [HttpPost]
        public ActionResult Delete(MenuViewModel model)
        {
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = MenuRepo.Delete(id);
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