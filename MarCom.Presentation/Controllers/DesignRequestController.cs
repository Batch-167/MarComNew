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
            UserViewModel model2 = GetIdByName(User.Identity.Name);
            DesignRequestViewModel model = new DesignRequestViewModel();
            //model.NameRequest = model2.Fullname;
            //ViewBag.DesignRequest = new SelectList(DesignRequestRepo.Get(), "Id", "Code");
            return PartialView("_Create", new DesignRequestViewModel());
        }

        [HttpPost]
        public ActionResult Create(DesignRequestViewModel model)
        {
            UserViewModel model2 = GetIdByName(User.Identity.Name);
            model.Request_By = model2.M_Employee_Id;
            model.Create_By = User.Identity.Name;
            ResultResponse result = DesignRequestRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);

        }

        public static UserViewModel GetIdByName(string name)
        {
            UserViewModel result = new UserViewModel();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_User
                          join e in db.M_Employee
                          on u.M_Employee_Id equals e.Id
                          where name == u.UserName
                          select new UserViewModel
                          {
                              Id = u.Id,
                              Password = u.PasswordHash,
                              M_Employee_Id = u.M_Employee_Id,
                              Fullname = e.First_Name + " " + e.Last_Name
                          }).FirstOrDefault();
            }
        return result;
        }
    }
}
