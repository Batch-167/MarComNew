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
            ViewBag.DesignRequest = new SelectList(DesignRequestRepo.Get(), "Id", "Code");
            return PartialView("_Create", new DesignRequestViewModel());
        }
    }
}
