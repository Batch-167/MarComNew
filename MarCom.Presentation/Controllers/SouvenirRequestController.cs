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
            UserViewModel result = GetIdByName(User.Identity.Name);
            SouvenirRequestViewModel model = new SouvenirRequestViewModel();
            ViewBag.Event = new SelectList(EventRepo.Get(), "Id", "Code");
            model.Request_By = result.M_Employee_Id;
            model.Name = result.Fullname;
            model.Code = SouvenirRequestRepo.GetNewCode();
            model.Request_Date = DateTime.Now;
            return PartialView("_Add", model);
        }

        [HttpPost]
        public ActionResult Add(SouvenirRequestViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = SouvenirRequestRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddItem ()
        {
            ViewBag.Souvenir = new SelectList(SouvenirRepo.Get(), "Id", "Name");
            SouvenirItemViewModel model = new SouvenirItemViewModel();
            return PartialView("_AddItem", model);
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