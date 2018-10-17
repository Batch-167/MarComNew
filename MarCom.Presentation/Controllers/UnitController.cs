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
    public class UnitController : Controller
    {
        // GET: Unit
        public ActionResult Index()
        {
            ViewBag.Unit1 = new SelectList(UnitRepo.Get(), "Code", "Code");
            ViewBag.Unit2 = new SelectList(UnitRepo.Get(), "Name", "Name");
            return View(UnitRepo.Get());
        }

        //[HttpPost]
        //public ActionResult Filter(UnitViewModel model)
        //{
        //    return PartialView("_List", UnitRepo.Filter(model));
        //}

        public ActionResult List()
        {
            return PartialView("_List", UnitRepo.Get());
        }

        public ActionResult Add()
        {
            UserViewModel result = UserRepo.GetIdByName(User.Identity.Name);
            if (result.Role=="Staff" || result.Role=="Admin")
            {
            return View("_Add", new UnitViewModel());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        [HttpPost]
        public ActionResult Add(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Create_By = User.Identity.Name;
                ResultResponse result = UnitRepo.Update(model);
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResultResponse result = UnitRepo.Update(model);
                result.Success = false;
                result.Message = "Please fill data correctly!";
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Edit(int id)
        {
            UnitViewModel model = UnitRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update_By = User.Identity.Name;
                ResultResponse result = UnitRepo.Update(model);
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResultResponse result = UnitRepo.Update(model);
                result.Success = false;
                result.Message = "Please fill data correctly!";
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            UnitViewModel model = UnitRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(UnitViewModel model)
        {
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = UnitRepo.Delete(id);
            if (UnitRepo.Delete(id))
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "Delete Success"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "Delete Failed"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int id)
        {
            UnitViewModel model = UnitRepo.GetById(id);
            return PartialView("_Details", model);
        }
    }
}