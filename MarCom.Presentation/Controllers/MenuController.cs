using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Routing;

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
            UserViewModel currentuser = UserRepo.GetIdByName(User.Identity.Name);
            if (currentuser.Role == "Staff" || currentuser.Role == "Admin")
            {
                ViewBag.Menu = new SelectList(MenuRepo.GetMenu(), "Id", "Controller");
                return PartialView("_Create", new MenuViewModel());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //POS        
        [HttpPost]
        public ActionResult Create(MenuViewModel model)
        {
            model.Create_By = User.Identity.Name;
            if (ModelState.IsValid)
            {
                ResultResponse result = MenuRepo.Update(model);
                return Json(new
                {
                    success = result.Success,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "A required data is still blank, Please fill Correctly"
                }, JsonRequestBehavior.AllowGet);
            }
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
            if (ModelState.IsValid)
            {
                model.Update_By = User.Identity.Name;
                ResultResponse result = MenuRepo.Update(model);
                return Json(new
                {
                    success = result.Success,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "A required data is still blank, Please fill Correctly"
                }, JsonRequestBehavior.AllowGet);
            }
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
            ResultResponse result = MenuRepo.Delete(id);
            if (result.Success)
            {
                return Json(new
                {
                    success = result.Success,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = result.Success,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}