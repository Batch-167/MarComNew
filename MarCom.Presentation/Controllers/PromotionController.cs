using MarCom.DataModel;
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

        public ActionResult Awal()
        {
            ViewBag.EventCode = new SelectList(EventRepo.Get(), "id", "Code");
            ViewBag.DesignCode = new SelectList(DesignRequestRepo.Get(), "id", "Code");
            PromotionViewModel model = new PromotionViewModel();
            return PartialView("_Awal", model);
        }

        public ActionResult Create()
        {
            ViewBag.EventCode = new SelectList(EventRepo.Get(), "id", "Code");
            ViewBag.DesignCode = new SelectList(DesignRequestRepo.Get(), "id", "Code");
            UserViewModel user1 = GetIdByName(User.Identity.Name);
            PromotionViewModel model = new PromotionViewModel();
            model.RequestBy = user1.Fullname;
            return PartialView("_Create", model);
        }

        public static UserViewModel GetIdByName(string name)
        {
            UserViewModel result = new UserViewModel();
            using (var db = new MarComContext())
            {
                result = (from u in db.M_User
                          join e in db.M_Employee on u.M_Employee_Id equals e.Id
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