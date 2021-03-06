﻿using MarCom.Repository;
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
    public class SouvenirController : Controller
    {
        // GET: Souvenir
        public ActionResult Index()
        {
            ViewBag.Souv1 = new SelectList(SouvenirRepo.Get(), "Code", "Code");
            ViewBag.Souv2 = new SelectList(SouvenirRepo.Get(), "Name", "Name");
            ViewBag.Souv3 = new SelectList(SouvenirRepo.Get(), "M_Unit_Id", "Unit");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(SouvenirViewModel model)
        {
            return PartialView("_List", SouvenirRepo.Filter(model));
        }

        public ActionResult List()
        {
            return PartialView("_List", SouvenirRepo.Get());
        }

        public ActionResult Create()
        {
            UserViewModel model = SouvenirRequestRepo.GetIdByName(User.Identity.Name);
            ViewBag.Souvenir = new SelectList(UnitRepo.Get(), "Id", "Name");
            if (model.Role == "Staff" || model.Role == "Admin")
            {
                return PartialView("_Create", new SouvenirViewModel());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //POS        
        [HttpPost]
        public ActionResult Create(SouvenirViewModel model)
        {
            model.Create_By = User.Identity.Name;
            if (ModelState.IsValid)
            {
                ResultResponse result = SouvenirRepo.Update(model);
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
                    message = "A required data is still blank, Please fill Correctly",
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            UserViewModel model2 = SouvenirRequestRepo.GetIdByName(User.Identity.Name);
            ViewBag.Unit = new SelectList(UnitRepo.Get(), "Id", "Name");
            SouvenirViewModel model = SouvenirRepo.GetById(id);
            if (model2.Role == "Staff" || model2.Role == "Admin")
            {
                return PartialView("_Edit", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //POST        
        [HttpPost]
        public ActionResult Edit(SouvenirViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update_By = User.Identity.Name;
                ResultResponse result = SouvenirRepo.Update(model);
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
                    message = "A required column is still blank, Please fill correctly",
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Show(int id)
        {
            SouvenirViewModel model = SouvenirRepo.GetById(id);
            return PartialView("_Show", model);
        }


        public ActionResult Delete(int id)
        {

            //ViewBag.Unit = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            SouvenirViewModel model = SouvenirRepo.GetById(id);
            return PartialView("_Delete", model);
        }

        //POST
        [HttpPost]
        public ActionResult Delete(SouvenirViewModel model)
        {

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            ResultResponse result = SouvenirRepo.Delete(id);
            if (result.Success)
            {
                return Json(new { success = result.Success, message = result.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = result.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}