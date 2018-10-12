using MarCom.DataModel;
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
    public class DesignRequestController : Controller
    {
        // GET: DesignRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Approve(int id)
        {
            ViewBag.Panel = "Approval Design Request";
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            DesignApproveViewModel model = DesignApproveRepo.GetById(id);
            return PartialView("_Approve", model);
        }

        [HttpPost]
        public ActionResult Approve(DesignApproveViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = DesignApproveRepo.Approve(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult List()
        {
            return PartialView("_List", DesignRequestRepo.Get());
        }

        //GET : New Product
        public ActionResult Create()
        {
            UserViewModel result = DesignRequestRepo.GetIdByName(User.Identity.Name);
            DesignRequestViewModel model = new DesignRequestViewModel();
            model.NameRequest = result.Fullname;
            model.Code = DesignRequestRepo.GetNewCode();
           // model.NameRequest = model2.Fullname;
            //model.NameRequest = model2.Fullname;
            ViewBag.DesignRequest = new SelectList(EventRepo.Get(), "Id", "Code");
            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult Create(DesignRequestViewModel model, List<DesignItemViewModel> item)
        {
            UserViewModel result1 = DesignRequestRepo.GetIdByName(User.Identity.Name);
            DesignRequestViewModel model1 = new DesignRequestViewModel();
            model1.Request_By = result1.M_Employee_Id;
            model1.Create_By = User.Identity.Name;
            ResultResponse result = DesignRequestRepo.Update(model, item);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddItem()
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            //ViewBag.Description = new SelectList(ProductRepo.Get(), "Id", "Description");
            DesignItemViewModel model = new DesignItemViewModel();
            return PartialView("_AddItem", model);
        }

        public ActionResult ProductList (int id)
        {
            return PartialView("_ProductList", DesignApproveRepo.Get(id));
        }
    }
}
