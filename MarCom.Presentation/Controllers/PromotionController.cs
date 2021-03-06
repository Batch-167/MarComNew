﻿using MarCom.DataModel;
using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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

        public ActionResult Create2(int designid, int eventid)
        {
            ViewBag.EventCode = new SelectList(EventRepo.Get(), "id", "Code");
            ViewBag.DesignCode = new SelectList(DesignRequestRepo.Get(), "id", "Code");

            UserViewModel model2 = PromotionRepo.GetIdByName(User.Identity.Name);

            PromotionViewModel model = new PromotionViewModel();
            model.RequestBy = model2.Fullname;
            model.T_Design_Id = designid;
            model.T_Event_Id = eventid;
            return PartialView("_Create2", model);
        }

        [HttpPost]
        public ActionResult Create2(PromotionViewModel model, List<PromotionItemViewModel> itemModel, List<PromotionItemFileViewModel> fileModel)
        {
            UserViewModel model2 = PromotionRepo.GetIdByName(User.Identity.Name);
            model.Create_By = User.Identity.Name;
            model.Request_By = model2.M_Employee_Id;
           
            ResultResponse result = PromotionRepo.Update(model, itemModel, fileModel);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        //Get Create 3
        public ActionResult Create3(int eventid)
        {
            UserViewModel model2 = PromotionRepo.GetIdByName(User.Identity.Name);

            ViewBag.EventCode = new SelectList(EventRepo.Get(), "id", "Code");
            PromotionViewModel model = new PromotionViewModel();
            model.RequestBy = model2.Fullname;
            model.T_Event_Id = eventid;
            return PartialView("_Create3", model);
        }

        //Post Create 3
        [HttpPost]
        public ActionResult Create3(PromotionViewModel model, List<PromotionItemFileViewModel> fileModel)
        {
            UserViewModel model2 = PromotionRepo.GetIdByName(User.Identity.Name);
            model.Create_By = User.Identity.Name;
            model.Request_By = model2.M_Employee_Id;
            ResultResponse result = PromotionRepo.UpdateCreate3(model, fileModel, model.Id);
            
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
            PromotionViewModel model = PromotionRepo.GetById(id);
            ViewBag.Employee = new SelectList(PromotionRepo.GetStaff(), "Id", "Full_Name");
            UserViewModel currentuser = PromotionRepo.GetIdByName(User.Identity.Name);

            if (model.FlagDesign == "Yes")
            {
                return PartialView("_Approve", model);
            }
            else
            {
                return PartialView("_ApproveNo", model);
            }

            if (currentuser.Role=="Admin")
            {
                ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "FullName");
                return PartialView("_Approve", PromotionRepo.GetById(id));
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
            
        }

        public ActionResult ApproveNo(int id)
        {
            return PartialView("_ApproveNo", PromotionRepo.GetById(id));
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
            if (model.Flag_Design=="1")
            {
                return PartialView("_Edit", model);
            }
            else
            {
                return PartialView("_EditNo", model);
            }
            
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

        public ActionResult EditList(int id)
        {
            List<PromotionItemFileViewModel> model = PromotionRepo.GetIdFile(id);
            return PartialView("_EditList", model);
        }
        //POST: Edit
        [HttpPost]
        public ActionResult Edit(PromotionViewModel model, List<PromotionItemViewModel> itemModel, List<PromotionItemFileViewModel> fileModel)
        {
            model.Update_By = User.Identity.Name;
            if (model.Flag_Design=="1")
            {
                ResultResponse result = PromotionRepo.Update(model, itemModel, fileModel);
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResultResponse result2 = PromotionRepo.UpdateCreate3(model, fileModel, model.Id);
                return Json(new
                {
                    success = result2.Success,
                    entity = model,
                    message = result2.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }


        //UPLOAD IMAGE

        public ActionResult UploadIndex()
        {
            return View();
        }

        private bool isValidContentType(string contenttype)
        {
            return contenttype.Equals("image/png") ||  contenttype.Equals("image/jpg") || contenttype.Equals("image/jpeg") ||contenttype.Equals("image/gif");
        }

        private bool isValidSize(int ContentLength)
        {
            return ((ContentLength / 1024) / 1024) < 1; //1MB
        }

        [HttpPost]
        public ActionResult UploadProcess(HttpPostedFileBase photo)
        {
            if (!isValidContentType(photo.ContentType))
            {
                ViewBag.Error = "Only JPG, JPEG, GIF files are allowed";
                return View("UploadIndex");
            }
            else if (!isValidSize(photo.ContentLength))
            {
                ViewBag.Error = "Your file too large";
                return View("UploadIndex");
            }
            else
            {
                if (photo.ContentLength>0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadImage"), fileName);
                    var extension = Path.GetExtension(photo.FileName);
                    var size = photo.ContentLength;
                    photo.SaveAs(path);
                    ViewBag.fileName = photo.FileName;
                }
                 
           
            return View("UploadSuccess");
            }
        }
    }
}