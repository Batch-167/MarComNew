﻿using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class SouvenirStockController : Controller
    {
        // GET: SouvenirStock
        public ActionResult Index()
        {
            ViewBag.Received = new SelectList(SouvenirStockRepo.Get(), "Id", "R_Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(SouvenirStockViewModel model)
        {
            return PartialView("_List", SouvenirStockRepo.Filter(model));
        }

        public ActionResult List()
        {
            return PartialView("_List", SouvenirStockRepo.Get());
        }

        public ActionResult ListItem(int id)
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            List<SouvenirItemViewModel> model = SouvenirStockRepo.GetItem(id);
            return PartialView("_ListItem", model);
        }

        public ActionResult AddItem()
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            SouvenirItemViewModel model = new SouvenirItemViewModel();
            return PartialView("_AddItem", model);
        }

        public ActionResult Add()
        {
            UserViewModel result = UserRepo.GetIdByName(User.Identity.Name);
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            SouvenirStockViewModel model = new SouvenirStockViewModel();
            model.Code = SouvenirStockRepo.GetNewCode();
            if (result.Role=="Staff" || result.Role=="Admin")
            {
                return PartialView("_Add", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        [HttpPost]
        public ActionResult Add(SouvenirStockViewModel model, List<SouvenirItemViewModel> item)
        {
            if (ModelState.IsValid)
            {
                model.Create_By = User.Identity.Name;
                ResultResponse result = SouvenirStockRepo.Update(model, item);
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "wew"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            UserViewModel result = UserRepo.GetIdByName(User.Identity.Name);
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            SouvenirStockViewModel model = SouvenirStockRepo.GetById(id);
            if (result.Role == "Staff" || result.Role == "Admin")
            {
                return PartialView("_Edit", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        [HttpPost]
        public ActionResult Edit(SouvenirStockViewModel model, List<SouvenirItemViewModel> item)
        {
            if (ModelState.IsValid)
            {
                model.Update_By = User.Identity.Name;
                SouvenirStockRepo.DeleteItem(model.Id);
                ResultResponse result = SouvenirStockRepo.Update(model, item);
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResultResponse result = SouvenirStockRepo.Update(model, item);
                return Json(new
                {
                    success = false,
                    message = "Wew"
                }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DetailItem(int id)
        {
            List<SouvenirItemViewModel> model = SouvenirStockRepo.GetItem(id);
            return PartialView("_DetailItem", model);
        }

        public ActionResult Details(int id)
        {
            SouvenirStockViewModel model = SouvenirStockRepo.GetById(id);
            return PartialView("_Details", model);
        }
    }
}