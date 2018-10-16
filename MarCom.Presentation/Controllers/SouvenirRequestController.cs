﻿using MarCom.DataModel;
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
    public class SouvenirRequestController : Controller
    {
        // GET: SouvenirRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", SouvenirRequestRepo.Get());
        }

        public ActionResult Add()
        {
            UserViewModel result = SouvenirRequestRepo.GetIdByName(User.Identity.Name);
            SouvenirRequestViewModel model = new SouvenirRequestViewModel();
            ViewBag.Event = new SelectList(EventRepo.Get(), "Id", "Code");
            model.Request_By = result.M_Employee_Id;
            model.Name = result.Fullname;
            model.Code = SouvenirRequestRepo.GetNewCode();
            model.Request_Date = DateTime.Now;
            if (result.Role == "Staff")
            {
                return PartialView("_Add", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        [HttpPost]
        public ActionResult Add(SouvenirRequestViewModel model, List<SouvenirItemViewModel> item)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = SouvenirRequestRepo.Update(model, item);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddItem()
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            SouvenirItemViewModel model = new SouvenirItemViewModel();
            return PartialView("_AddItem", model);
        }

        public ActionResult Edit(int id)
        {
            UserViewModel result = SouvenirRequestRepo.GetIdByName(User.Identity.Name);
            SouvenirRequestViewModel model = SouvenirRequestRepo.GetById(id);
            if (result.Role == "Staff")
            {
                return PartialView("_Edit", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        [HttpPost]
        public ActionResult Edit(SouvenirRequestViewModel model, List<SouvenirItemViewModel> item)
        {
            model.Update_By = User.Identity.Name;
            SouvenirRequestRepo.DeleteItem(model.Id);
            ResultResponse result = SouvenirRequestRepo.Update(model, item);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListItem(int id)
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            List<SouvenirItemViewModel> model = SouvenirRequestRepo.GetItem(id);
            return PartialView("_ListItem", model);
        }

        public ActionResult Received(int id)
        {
            UserViewModel result = SouvenirRequestRepo.GetIdByName(User.Identity.Name);
            SouvenirRequestViewModel model = SouvenirRequestRepo.GetById(id);
            if (result.Role == "Staff")
            {
                return PartialView("_Received", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        public ActionResult SouSettApproved(int id)
        {
            return PartialView("_SouSettApproved", SouvenirRequestRepo.GetById(id));
        }

        public ActionResult SouSettItemApproved(int id)
        {
            return PartialView("_SouSettItemApproved", SouvenirRequestRepo.GetItem(id));
        }


        //APPROVE
        public ActionResult Approve(int id)
        {
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            SouvenirRequestViewModel model = SouvenirRequestRepo.GetById(id);
            return PartialView("_Approve", model);
        }

        //APPROVE LIST ITEM
        public ActionResult ApproveList(int id)
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            List<SouvenirItemViewModel> model = SouvenirRequestRepo.GetItem(id);
            return PartialView("_ApproveList", SouvenirRequestRepo.GetItem(id));
        }

        //APPROVE POST
        [HttpPost]
        public ActionResult Approve(SouvenirRequestViewModel model)
        {
            UserViewModel model2 = SouvenirRequestRepo.GetIdByName(User.Identity.Name);
            model.Approved_By = model2.M_Employee_Id;
            ResultResponse result = SouvenirRequestRepo.Approve(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }
    }
}