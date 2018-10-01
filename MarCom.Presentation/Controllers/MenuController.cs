using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
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
            if (MenuRepo.Delete(id))
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