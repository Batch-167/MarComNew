using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", UserRepo.Get());
        }

        public ActionResult Add()
        {
            //ViewBag.Role = new SelectList(RoleRepo.Get(), "Id", "Name");
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "Fullname");
            return View("_Add", new UserViewModel());
        }


        [HttpPost]
        public ActionResult Add(UserViewModel model)
        {
            ResultResponse result = UserRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            //ViewBag.Role = new SelectList(RoleRepo.Get(), "Id", "Name");
            ViewBag.Employee = new SelectList(EmployeeRepo.Get(), "Id", "Fullname");
            UserViewModel model = UserRepo.GetById(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            ResultResponse result = UserRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            UserViewModel model = UserRepo.GetById(id);
            return PartialView("_Delete", model);
        }


        [HttpPost]
        public ActionResult Delete(UserViewModel model)
        {
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            if (UserRepo.Delete(id))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Details(int id)
        {
            UserViewModel model = UserRepo.GetById(id);
            return PartialView("_Details", model);
        }
    }
}