using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            ViewBag.Role1 = new SelectList(RoleRepo.Get(), "Code", "Code");
            ViewBag.Role2 = new SelectList(RoleRepo.Get(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(RoleViewModel model)
        {
            return PartialView("_List", RoleRepo.Filter(model));
        }

        public ActionResult List()
        {
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_List", RoleRepo.Get());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }

        }


        //GET : New Role
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(RoleRepo.Get(), "Id", "Name");
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_Create", new RoleViewModel());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }

        }

        //POS
        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            model.Create_By = User.Identity.Name;
            ResultResponse result = RoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult View(int id)
        {
            RoleViewModel model = RoleRepo.GetById(id);
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_View", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Role = new SelectList(RoleRepo.Get(), "Id", "Name");
            RoleViewModel model = RoleRepo.GetById(id);
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return View("_Edit", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //POST
        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            model.Create_By = User.Identity.Name;
            model.Update_By = User.Identity.Name;
            ResultResponse result = RoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id)
        {

            RoleViewModel model = RoleRepo.GetById(id);
            UserViewModel access = DesignApproveRepo.GetIdByName(User.Identity.Name);
            if (access.Role == "Admin")
            {
                return PartialView("_Delete", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //POST
        [HttpPost]
        public ActionResult Delete(RoleViewModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = RoleRepo.Delete(id);

            if (result)
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "Data Deleted! Data Role has been deleted!"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = result,
                    entity = "",
                    message = "Delete Failed!!!"
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}