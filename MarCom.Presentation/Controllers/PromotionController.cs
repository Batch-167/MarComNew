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

        public ActionResult Create2()
        {
            ViewBag.EventCode = new SelectList(EventRepo.Get(), "id", "Code");
            ViewBag.DesignCode = new SelectList(DesignRequestRepo.Get(), "id", "Code");
            UserViewModel model2 = PromotionRepo.GetIdByName(User.Identity.Name);
            PromotionViewModel model = new PromotionViewModel();
            model.RequestBy = model2.Fullname;
            return PartialView("_Create2", model);
        }

        [HttpPost]
        public ActionResult Create2(PromotionViewModel model, List<PromotionItemViewModel> itemModel, List<PromotionItemFileViewModel> fileModel)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = PromotionRepo.Update(model, itemModel,fileModel);            
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create3()
        {
            ViewBag.EventCode = new SelectList(EventRepo.Get(), "id", "Code");
            UserViewModel model2 = PromotionRepo.GetIdByName(User.Identity.Name);
            PromotionViewModel model = new PromotionViewModel();
            model.RequestBy = model2.Fullname;
            return PartialView("_Create3", model);
        }

        //untuk view design request
        public ActionResult DesignReq(int id)
        {
            DesignRequestViewModel model = PromotionRepo.GetDesReq(id);
            return PartialView("_DesignReq", model);
        }

        //untuk view design request item
        public ActionResult DesignReqItem(int id)
        {
            List<PromotionItemViewModel> model = PromotionRepo.GetDesReqItem(id);
            return PartialView("_DesignReqItem", model);
        }
        
    }
}