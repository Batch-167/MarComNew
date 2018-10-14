﻿using MarCom.DataModel;
using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class PromotionController : Controller
    {
        // GET: Promotion
        public ActionResult Index()
        {
            return View();
        }

        //GET: List
        public ActionResult List()
        {
            return PartialView("_List", PromotionRepo.Get());
        }

        public ActionResult AddItem()
        {
            //ViewBag.Promotion = new SelectList(PromotionRepo.Get(), "Id", "Name");
            PromotionItemFileViewModel model = new PromotionItemFileViewModel();
            return PartialView("_AddItem", model);
        }

        public ActionResult Create()
        {
            ViewBag.EventCode = new SelectList(EventRepo.Get(), "id", "Code");
            ViewBag.DesignCode = new SelectList(DesignRequestRepo.Get(), "id", "Code");
            PromotionViewModel model = new PromotionViewModel();
            return PartialView("_Create", model);
        }

        public ActionResult Create2(int designid)
        {
            ViewBag.EventCode = new SelectList(EventRepo.Get(), "id", "Code");
            ViewBag.DesignCode = new SelectList(DesignRequestRepo.Get(), "id", "Code");

            UserViewModel model2 = PromotionRepo.GetIdByName(User.Identity.Name);

            PromotionViewModel model = new PromotionViewModel();
            model.RequestBy = model2.Fullname;
            model.T_Design_Id = designid;
            return PartialView("_Create2", model);
        }

        [HttpPost]
        public ActionResult Create2(PromotionViewModel model, List<PromotionItemViewModel> itemModel, List<PromotionItemFileViewModel> fileModel)
        {
            UserViewModel model2 = PromotionRepo.GetIdByName(User.Identity.Name);
            model.Create_By = User.Identity.Name;
            model.Request_By = model2.M_Employee_Id;


            //untuk upload
            //string fileName =Path.GetfileNameWithoutExtension(fileModel.imageFile.fileName)

            ResultResponse result = PromotionRepo.Update(model, itemModel);
            ResultResponse result2 = PromotionRepo.UpdateFile(fileModel, model.Id);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //untuk view design request, menu ADD
        public ActionResult DesignReq(int id)
        {
            DesignRequestViewModel model = PromotionRepo.GetDesReq(id);
            return PartialView("_DesignReq", model);
        }

        //untuk view design request item, menu ADD
        public ActionResult DesignReqItem(int id)
        {
            List<PromotionItemViewModel> model = PromotionRepo.GetDesReqItem(id);
            return PartialView("_DesignReqItem", model);
        }

        //View Approve
        public ActionResult Approve(int id)
        {

            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "FullName");
            return PartialView("_Approve", PromotionRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Approve(PromotionViewModel model)
        {
            UserViewModel model1 = PromotionRepo.GetIdByName(User.Identity.Name);
            model.Approved_By = model1.M_Employee_Id;
            ResultResponse result = PromotionRepo.Approve(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //View Design, untuk Approval
        public ActionResult ViewDesign(int id)
        {
            return PartialView("_ViewDesign", PromotionRepo.GetId(id));
        }

        //View Design Item, untuk Approval
        public ActionResult ViewDesignItem(int id)
        {
            return PartialView("_ViewDesignItem", PromotionRepo.GetItemId(id));
        }

        //View Promotion Item File, untuk Approval
        public ActionResult ViewPromotionItemFile(int id)
        {
            return PartialView("_ViewPromotionItemFile", PromotionRepo.GetIdFile(id));
        }
        //GET: View Edit
        public ActionResult Edit(int id)
        {
            PromotionViewModel model = PromotionRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        //View Design, untuk Edit
        public ActionResult EditDesign(int id)
        {
            return PartialView("_EditDesign", PromotionRepo.GetId(id));
        }

        //View Design Item, untuk Edit
        public ActionResult EditDesignItem(int id)
        {
            List<PromotionItemViewModel> model = PromotionRepo.GetItemId(id);
            return PartialView("_EditDesignItem", model);
        }
        //POST: Edit
        [HttpPost]
        public ActionResult Edit(PromotionViewModel model, List<PromotionItemViewModel> itemModel, List<PromotionItemFileViewModel> fileModel)
        {
            model.Update_By = User.Identity.Name;
            ResultResponse result = PromotionRepo.Update(model, itemModel);
            ResultResponse result2 = PromotionRepo.UpdateFile(fileModel, model.Id);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        //UPLOAD IMAGE
        [HttpGet]
        public ActionResult UploadImage()
        {
            return View();
        }
    }
}