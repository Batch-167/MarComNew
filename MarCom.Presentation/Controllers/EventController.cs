using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MarCom.DataModel;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Approve(int id)
        {
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "First_Name");
            EventApproveViewModel model = EventApproveRepo.GetById(id);
            return PartialView("_Approve", model);
        }

        [HttpPost]
        public ActionResult Approve(EventApproveViewModel model)
        {
            UserViewModel model3 = GetIdByName(User.Identity.Name);
            model.Approved_By = model3.M_Employee_Id;
            ResultResponse result = EventApproveRepo.Approve(model);
            return Json(new
            {
                success = result.Success,
                entity = model,

                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult List()
        {
            return PartialView("_List", EventRepo.Get());
        }

        public ActionResult Add()
        {
            //string name = User.Identity.Name;
            UserViewModel model2 = GetIdByName(User.Identity.Name);
            EventViewModel model = new EventViewModel();           
            model.NameRequest = model2.Fullname;
            return PartialView("_Add", model);
        }

        [HttpPost]

        public ActionResult Add(EventViewModel model)
        {
            UserViewModel model2 = GetIdByName(User.Identity.Name);
            model.Request_By = model2.M_Employee_Id;
            model.Create_By = User.Identity.Name;
            ResultResponse result = EventRepo.Update(model);
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
                              Fullname = e.First_Name +" "+ e.Last_Name
                              

                          }
                         ).FirstOrDefault();
            }
            return result;
        }

        public static EmployeeViewModel GetIdByNames(string name)
        {
            EmployeeViewModel result = new EmployeeViewModel();
            using (var db = new MarComContext())
            {
                result = (from e in db.M_Employee
                          where name == e.First_Name+" "+e.Last_Name
                          select new EmployeeViewModel
                          {
                              Id = e.Id,
                           }
                         ).FirstOrDefault();
            }
            return result;
        }

        public ActionResult Edit (int id)
        {
            return PartialView("_Edit", EventRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(EventViewModel model)
        {
            EmployeeViewModel model2 = GetIdByNames(model.NameRequest);
            model.Request_By = model2.Id;
            model.Update_By = User.Identity.Name;
            ResultResponse result = EventRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            return PartialView("_Details", EventRepo.GetById(id));
        }
    }
}