using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Routing;

namespace MarCom.Presentation.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            ViewBag.Company = new SelectList(CompanyRepo.Get(), "Code", "Code");
            ViewBag.Company1 = new SelectList(CompanyRepo.Get(), "Name", "Name");
            return View(CompanyRepo.Get());
        }

        [HttpPost]
        public ActionResult Filter(CompanyViewModel model)
        {
            return PartialView("_List", CompanyRepo.Filter(model));
        }

        public ActionResult List()
        {
            return PartialView("_List", CompanyRepo.Get());
        }

        //GET
        public ActionResult Create()
        {
            UserViewModel model = CompanyRepo.GetIdByName(User.Identity.Name);

            if (model.Role == "Admin")
            {
                return PartialView("_Create", new CompanyViewModel());
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }

        }

        //POST
        [HttpPost]
        public ActionResult Create(CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResultResponse result = CompanyRepo.Update(model);
                model.Create_By = User.Identity.Name;
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResultResponse result = new ResultResponse();
                result.Success = false;
                result.Message = "Theres blank column, Please fill data correctly !";
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        //GET

        public ActionResult Edit(int id)
        {
            UserViewModel modul = CompanyRepo.GetIdByName(User.Identity.Name);
            if (modul.Role == "Admin")
            {
                CompanyViewModel model = CompanyRepo.GetById(id);
                return PartialView("_Edit", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        //POST
        [HttpPost]
        public ActionResult Edit(CompanyViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.Update_By = User.Identity.Name;
                ResultResponse result = CompanyRepo.Update(model);
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResultResponse result = new ResultResponse();
                result.Success = false;
                result.Message = "Theres blank column, Please fill data correctly !";
                return Json(new
                {
                    success = result.Success,
                    entity = model,
                    message = result.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Delete(int id)
        {
            UserViewModel modul = CompanyRepo.GetIdByName(User.Identity.Name);
            if (modul.Role == "Admin")
            {
                CompanyViewModel model = CompanyRepo.GetById(id);
                return PartialView("_Delete", model);
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        [HttpPost]
        public ActionResult Delete(CompanyViewModel model)
        {
            UserViewModel modul = CompanyRepo.GetIdByName(User.Identity.Name);
            if (modul.Role == "Admin")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = ProductRepo.Delete(id);

            UserViewModel modul = CompanyRepo.GetIdByName(User.Identity.Name);
            if (modul.Role == "Admin")
            {
                if (CompanyRepo.Delete(id))
                {
                    return Json(new
                    {
                        success = result,
                        entity = "",
                        message = "delete success"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        success = result,
                        entity = "",
                        message = "delete fail"
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }

        public ActionResult Details(int id)
        {
            CompanyViewModel model = CompanyRepo.GetById(id);
            return PartialView("_Details", model);
        }

    }
}